using System.ComponentModel.DataAnnotations;
using PARCIAL1D.models;
using PARCIAL1D.utils;

namespace PARCIAL1D.Test.tests
{
    [TestClass]
    public class SillasValidationTest
    {
        [TestMethod]
        public void Sillas_error()
        {
            //setup
            var sillasValidation = new SillasValidation();
            var sillas = -1;
            var validationContext = new ValidationContext(new Mesas { SillasMesa = sillas });

            //run
            var result = sillasValidation.GetValidationResult(sillas, validationContext);

            //verify
            Assert.AreEqual("El número de sillas debe ser mayor a 0", result.ErrorMessage);
        }

        [TestMethod]
        public void Sillas_success()
        {
            //setup
            var sillasValidation = new SillasValidation();
            var sillas = 1;
            var validationContext = new ValidationContext(new Mesas { SillasMesa = sillas });

            //run
            var result = sillasValidation.GetValidationResult(sillas, validationContext);

            //verify
            Assert.AreEqual(ValidationResult.Success, result);
        }
    }
}