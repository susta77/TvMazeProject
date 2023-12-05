using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TvMaze.Project.Models;

namespace TvMaze.Project.Services
{
    public class JwtService : IJwtService
    {

        // Método para manejar errores generados intencionalmente (solo para pruebas).
        public void GenerateErrorTest()
        {
            int numerator = 10;
            int denominator = 0;

            // Esta línea generará un error CS0020 (División por cero constante)
            int result = numerator / denominator;
        }

        public Token GenerarToken(string keySecret, IEnumerable<Claim> claims = null, JwtUser user = null)
        {
            //GenerateErrorTest();
            // Crear el descriptor del token con la clave secreta, IDColaborador y otros claims
            var tokenDescriptor = CrearTokenDescriptor(keySecret, claims);

            // Generar el token a partir del descriptor
            var token = GenerarToken(tokenDescriptor);

            // Escribir el token como cadena
            var tokenString = EscribirToken(token);

            // Decodificar el token y convertirlo a una clase Token
            string jsonString = DecodificarToken(tokenString);
            Token myToken = JsonConvert.DeserializeObject<Token>(jsonString);
            myToken.tokenString = tokenString;
            myToken.Json = jsonString;
            return myToken;
        }

        private SecurityTokenDescriptor CrearTokenDescriptor(string keySecret, IEnumerable<Claim> claims)
        {
            // Convertir la clave secreta a bytes
            var key = Encoding.ASCII.GetBytes(keySecret);

            // Configurar el descriptor del token
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("Show", ""),
                    // Puedes agregar más claims según sea necesario
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private JwtSecurityToken GenerarToken(SecurityTokenDescriptor tokenDescriptor)
        {
            // Crear un manejador de tokens JWT y generar el token
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor) as JwtSecurityToken;
        }

        private string EscribirToken(JwtSecurityToken token)
        {
            // Escribir el token como cadena
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public string DecodificarToken(string token)
        {
            try
            {
                // Extraer el payload del token y devolverlo como cadena
                var payload = ExtraerPayloadDesdeToken(token);
                return payload;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al decodificar el payload del token: {ex.Message}");
                return string.Empty;
            }
        }

        private string ExtraerPayloadDesdeToken(string token)
        {
            // Extraer y decodificar el payload desde el token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSlices = token.Split('.');

            if (tokenSlices.Length == 3)
            {
                var payloadBase64 = tokenSlices[1];
                var payloadBytes = Convert.FromBase64String(RellenarBase64(payloadBase64));
                return Encoding.UTF8.GetString(payloadBytes);
            }
            else
            {
                throw new InvalidOperationException("El token JWT no tiene un formato válido.");
            }
        }

        private string RellenarBase64(string base64)
        {
            // Agregar relleno si es necesario para alcanzar una longitud múltiple de 4
            while (base64.Length % 4 != 0)
            {
                base64 += "=";
            }
            return base64;
        }

        public Token ObtenerInformacionToken(ClaimsIdentity claims)
        {
            try
            {
                if (claims != null)
                {
                    // Crear un objeto Token con la información del token
                    var token = new Token
                    {
                        Json = ObtenerStringToken(claims)
                    };

                    return token;
                }
                else
                {
                    return new Token(); // O manejar null de alguna manera
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private int ObtenerIDColaboradorDesdeClaims(ClaimsIdentity claims)
        {
            // Obtener el valor del claim "IDColaborador" y convertirlo a entero
            var IDColaboradorClaim = claims.FindFirst("IDColaborador");
            return IDColaboradorClaim != null && int.TryParse(IDColaboradorClaim.Value, out int idColaborador)
                ? idColaborador
                : 0;
        }

        public string ObtenerStringToken(ClaimsIdentity claims)
        {
            var claimsDictionary = claims.Claims.ToDictionary(c => c.Type, c => c.Value);
            return JsonConvert.SerializeObject(claimsDictionary);
        }
    }
}