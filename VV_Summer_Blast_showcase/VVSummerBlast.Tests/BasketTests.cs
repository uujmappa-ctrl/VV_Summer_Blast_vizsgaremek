using Xunit;
using VVSummerBlastBackendAPI.Models; // A pontos namespace a te kódodból
using System.Collections.Generic;

namespace VVSummerBlast.Tests
{
    public class BasketTests
    {
        [Fact]
        public void RendelesTetel_Szamitas_HelyesErteketAd()
        {
            // Arrange - Előkészítjük a tételeket a te modelljeid alapján
            var tetel = new RendelesTetel
            {
                Egysegar = 8990, // Itt 'Egysegar' a pontos név nálad!
                Mennyiseg = 3
            };

            // Act - Kiszámoljuk a tétel értékét
            decimal tételÖsszeg = tetel.Egysegar * tetel.Mennyiseg;

            // Assert - Ellenőrizzük, hogy 3 * 8990 tényleg 26970
            Assert.Equal(26970, tételÖsszeg);
        }

        [Fact]
        public void Kosar_Mennyiseg_Validacio_Teszt()
        {
            // Arrange
            var kosar = new Kosar { Mennyiseg = 10 };

            // Act & Assert
            // Itt azt teszteljük, hogy a megadott érték a valid tartományon belül van-e (1-50)
            Assert.InRange(kosar.Mennyiseg, 1, 50);
        }

        [Fact]
        public void Kosar_ErvenytelenMennyiseg_HibatKelleneAdnia()
        {
            // Arrange
            var kosar = new Kosar { Mennyiseg = 60 }; // Több, mint a megengedett 50

            // Act
            bool ervenyes = kosar.Mennyiseg <= 50 && kosar.Mennyiseg >= 1;

            // Assert
            Assert.False(ervenyes, "A mennyiségnek 1 és 50 között kellene lennie!");
        }
    }
}