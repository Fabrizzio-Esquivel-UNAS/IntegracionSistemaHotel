using System.ComponentModel.DataAnnotations;
using ProductosAPI.Models;
using Xunit;

namespace ProductosAPI.Tests
{
    public class HuespedModelTests
    {
        [Fact]
        public void Huesped_ValidModel_ReturnsNoErrors()
        {
            var huesped = new Huesped
            {
                NombreCompleto = "Juan Perez",
                DocumentoIdentidad = "12345678",
                Telefono = "123456789",
                Correo = "juan@example.com"
            };

            var results = ValidationHelper.ValidateModel(huesped);

            Assert.Empty(results);
        }

        [Fact]
        public void Huesped_MissingNombreCompleto_ReturnsError()
        {
            var huesped = new Huesped
            {
                DocumentoIdentidad = "12345678"
            };

            var results = ValidationHelper.ValidateModel(huesped);

            Assert.Contains(results, r => r.MemberNames.Contains("NombreCompleto") && r.ErrorMessage.Contains("El nombre completo es obligatorio"));
        }

        [Fact]
        public void Huesped_ShortNombreCompleto_ReturnsError()
        {
            var huesped = new Huesped
            {
                NombreCompleto = "Jo",
                DocumentoIdentidad = "12345678"
            };

            var results = ValidationHelper.ValidateModel(huesped);

            Assert.Contains(results, r => r.MemberNames.Contains("NombreCompleto") && r.ErrorMessage.Contains("El nombre completo debe tener entre 3 y 100 caracteres"));
        }

        [Fact]
        public void Huesped_MissingDocumentoIdentidad_ReturnsError()
        {
            var huesped = new Huesped
            {
                NombreCompleto = "Juan Perez"
            };

            var results = ValidationHelper.ValidateModel(huesped);

            Assert.Contains(results, r => r.MemberNames.Contains("DocumentoIdentidad") && r.ErrorMessage.Contains("El documento de identidad es obligatorio"));
        }

        [Fact]
        public void Huesped_InvalidCorreo_ReturnsError()
        {
            var huesped = new Huesped
            {
                NombreCompleto = "Juan Perez",
                DocumentoIdentidad = "12345678",
                Correo = "invalid-email"
            };

            var results = ValidationHelper.ValidateModel(huesped);

            Assert.Contains(results, r => r.MemberNames.Contains("Correo") && r.ErrorMessage.Contains("El formato del correo"));
        }
    }
}
