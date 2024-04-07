using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateLibrary.Data.Models;

namespace PrivateLibrary.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            var books = new List<Book>();

            var book = new Book
            {
                Id = 1,
                Title = "Под игото",
                Author = "Иван Вазов",
                IsTaken = false,
                CostPerDay = 4,
                ISBN = "9786192510244",
                Image = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/p/o/19071533ecfae7e0bc6c0df761f2b77a/pod-igoto-helikon-chervena-koritsa-30.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 2,
                Title = "Тютюн",
                Author = "Димитър Димов",
                IsTaken = false,
                CostPerDay = 5,
                ISBN = "9789547385289",
                Image = "https://i4.helikon.bg/products/4697/14/144697/3613030_b.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 3,
                Title = "Топ мистериите на България",
                Author = "Слави Панайотов",
                IsTaken = false,
                CostPerDay = 7,
                ISBN = " 9786197511017",
                Image = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/t/o/e563b1cc68b7a08799821bf41c795b65/top-misteriite-na-balgariya-30.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 4,
                Title = "Железният светилник",
                Author = "Димитър Талев",
                IsTaken = false,
                CostPerDay = 2,
                ISBN = "9789549232813",
                Image = "https://hermesbooks.bg/media/catalog/product/cache/e533a3e3438c08fe7c51cedd0cbec189/j/e/jelezniat_svetilnik_hrm_2_20200901160342.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 5,
                Title = "Диви Разкази",
                Author = "Николай Хайтов",
                IsTaken = false,
                CostPerDay = 5,
                ISBN = " 9789540904382",
                Image = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/d/i/705d5f90e5fb5ee3725b8922b76073b7/sachineniya-v-17-toma---tom-2--divi-razkazi-31.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 6,
                Title = "Великият Гетсби",
                Author = "Ф. Скот Фицджералд",
                IsTaken = false,
                CostPerDay = 8,
                ISBN = "9789542819387",
                Image = "https://www.ciela.com/media/catalog/product/cache/332cf88b637d37883ec9cea105be873e/g/e/getsbi-koritsa.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 7,
                Title = "Към себе си",
                Author = "Марк Аврелий",
                IsTaken = false,
                CostPerDay = 5,
                ISBN = "9789542838654",
                Image = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/m/a/770200d2b28cce79f22d92eb4b8c13ba/mark-avreliy--kam-sebe-si-luksozno-izdanie-30.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 8,
                Title = "Престъпление и наказание",
                Author = "Фьодор М. Достоевски",
                IsTaken = false,
                CostPerDay = 9,
                ISBN = "9789540907680",
                Image = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/p/r/fe0c5f747aa7d7feab7a3dd45388a570/prestaplenie-i-nakazanie-30.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 9,
                Title = "1984",
                Author = "Джордж Оруел",
                IsTaken = false,
                CostPerDay = 6,
                ISBN = "9789542833734",
                Image = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/1/9/4a0dca141d62410d50fb2615ff15ad73/1984-siela-30.jpg"
            };
            books.Add(book);

            book = new Book
            {
                Id = 10,
                Title = "Фермата на животните",
                Author = "Джордж Оруел",
                IsTaken = false,
                CostPerDay = 5,
                ISBN = "9789542833703",
                Image = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/f/e/ee665f8b54103f6b17870252d8ac34ff/fermata-na-zhivotnite-siela-30.jpg"
            };
            books.Add(book);

            builder.HasData(books);
        }
    }
}
