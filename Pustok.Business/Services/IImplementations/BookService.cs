using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;
using WebApplication6.CustomExceptions.BookException;
using WebApplication6.Helpers;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;
using WebApplication6.Services.Interfaces;

namespace WebApplication6.Services.IImplementations
{
    public class BookService : IBookService
    {


        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IBookTagRepository _bookTagsRepository;
        private readonly IBookImageRepository _bookImagesRepository;

        public BookService(IBookRepository bookRepository,
                           IGenreRepository genreRepository,
                           IAuthorRepository authorRepository,
                           ITagRepository tagRepository,                       
                           IBookImageRepository bookImageRepository,
                           IBookTagRepository bookTagRepository)

        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
            _tagRepository = tagRepository;
            _bookTagsRepository = bookTagRepository;
          
            _bookImagesRepository = bookImageRepository;
        }

        public async Task CreateAsync(Book book)
        {

          

            if (!_authorRepository.Table.Any(a => a.Id == book.AuthorId))
            {
                throw new InvalidAuthorid("AuthorId", "author is not found!!!");
            }

            if (!_genreRepository.Table.Any(g => g.Id == book.GenreId))
            {
                throw new invalidGenreId("GenreId", "genre is not found!!!");
            }

            bool check = false;

            if (book.TagIds != null)
            {
                foreach (var item in book.TagIds)
                {
                    if (!_tagRepository.Table.Any(t => t.Id == item))
                    {
                        check = true;
                        break;
                    }

                }
            }

            if (check)
            {
                throw new InvalidTagId("TagId", "tag is not found!!!");
            }
            else
            {
                if (book.TagIds != null)
                {
                    foreach (var item in book.TagIds)
                    {
                        BookTag bookTag = new BookTag()
                        {
                            Book = book,
                            TagId = item,
                        };

                        await _bookTagsRepository.CreateAsync(bookTag);
                    }
                }
            }


            if (book.FaceImage != null)
            {

                if (book.FaceImage.ContentType != "image/png" && book.FaceImage.ContentType != "image/jpeg")
                {
                    throw new InvalidContenttype("FaceImage", "ancaq sekil yukle");
                    
                }

                if (book.FaceImage.Length > 1048576)
                {
                    throw new InvalidImgSize("FaceImage", "1 mb dan az yukle pul yazir ");
                    
                }
                string path = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                string newFileName = Helper.GetFileName(path, "upload", book.FaceImage);
                BookImage bookImage = new BookImage
                {
                    book = book,
                    ImageUrl = newFileName,
                    IsPoster = true,
                };

                await _bookImagesRepository.CreateAsync(bookImage);
            };

            if (book.BackViewImg != null)
            {

                if (book.BackViewImg.ContentType != "image/png" && book.BackViewImg.ContentType != "image/jpeg")
                {
                    throw new InvalidContenttype("FaceImage", "ancaq sekil yukle");
                }

                if (book.BackViewImg.Length > 1048576)
                {
                    throw new InvalidImgSize("FaceImage", "1 mb dan az yukle pul yazir ");
                }
                string path = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                string newFileName = Helper.GetFileName(path, "upload", book.BackViewImg);
                BookImage bookImage = new BookImage
                {
                    book = book,
                    ImageUrl = newFileName,
                    IsPoster = false,
                };
               await _bookImagesRepository.CreateAsync( bookImage);
            };

            if (book.BookDetailImgs != null)
            {
                foreach (var img in book.BookDetailImgs)
                {
                    string fileName = img.FileName;
                    if (img.ContentType != "image/png" && img.ContentType != "image/jpeg")
                    {
                        throw new InvalidContenttype("FaceImage", "ancaq sekil yukle");
                    }

                    if (img.Length > 1048576)
                    {
                        throw new InvalidImgSize("FaceImage", "1 mb dan az yukle pul yazir ");
                    }
                    string path = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                    string newFileName = Helper.GetFileName(path, "upload", img);
                    BookImage bookImage = new BookImage
                    {
                        book = book,
                        ImageUrl = newFileName,
                        IsPoster = null,
                    };
                    await _bookImagesRepository.CreateAsync(bookImage);
                }
            }

            await _bookRepository.CreateAsync(book);
            await _bookRepository.CommitAsync();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task SoftDelete(int id)
        {
            var entity = await _bookRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            entity.IsDeleted = true;
            await _bookRepository.CommitAsync();
        }

        public async Task<List<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null)
        {
            return await _bookRepository.GetAllAsync(expression, "BookImages", "Author");
        }
        public async Task<List<Book>> GetAllRelatedBooksAsync(Book book)
        {
            return await _bookRepository.GetAllAsync(x => x.GenreId == book.GenreId && !x.IsDeleted && x.Id != book.Id, "Author", "BookImages");
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var entity = await _bookRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false, "Author", "BookImages", "BookTags.Tag", "Genre");

            if (entity is null) throw new NullReferenceException();

            return entity;
        }

        public async Task UpdateAsync(Book book)
        {
          

            Book existBook = await _bookRepository.GetByIdAsync(x => x.Id == book.Id);

            if (existBook == null) throw new InvalidNullreferance();

            if (!_bookImagesRepository.Table.Any(a => a.Id == book.AuthorId))
            {
                throw new InvalidAuthorid("AuthorId", "author is not found!!!");
            }

            if (!_bookImagesRepository.Table.Any(g => g.Id == book.GenreId))
            {
                throw new invalidGenreId("GenreId", "genre is not found!!!");
            }

            existBook.BookTags.RemoveAll(bt => !book.TagIds.Any(tId => tId == bt.TagId));

            foreach (var id in book.TagIds.Where(bt => !existBook.BookTags.Any(tId => bt == tId.TagId)))
            {
                BookTag bookTag = new BookTag()
                {
                    TagId = id,
                };

                existBook.BookTags.Add(bookTag);

            }


            if (book.FaceImage != null)
            {
                string folderPath = "upload";
                string pathh = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                string path = Path.Combine(pathh, folderPath, existBook.BookImages.FirstOrDefault(x => x.IsPoster == true).ImageUrl);

                existBook.BookImages.RemoveAll(bi => !book.BookImagesIds.Contains(bi.Id) && bi.IsPoster == true);


                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                if (book.FaceImage.ContentType != "image/png" && book.FaceImage.ContentType != "image/jpeg")
                {

                    throw new InvalidContenttype("FaceImage", "sekil yukle a bala");
                }

                if (book.FaceImage.Length > 1048576)
                {
                    throw new InvalidImgSize("FaceImage", "1 mb dan az yukle");
                }
              
                string newFileName = Helper.GetFileName(pathh, "upload", book.FaceImage);
                BookImage bookImage = new BookImage
                {
                    book = book,
                    ImageUrl = newFileName,
                    IsPoster = true,
                };
                existBook.BookImages.Add(bookImage);
            };

            if (book.BackViewImg != null)
            {
                string pathh = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                string folderPath = "upload";
                string path = Path.Combine(pathh, folderPath, existBook.BookImages.Where(x => x.IsPoster == false).FirstOrDefault().ImageUrl);
                existBook.BookImages.RemoveAll(x => !book.BookImagesIds.Contains(x.Id) && x.IsPoster == false);


                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                if (book.BackViewImg.ContentType != "image/png" && book.BackViewImg.ContentType != "image/jpeg")
                {
                    throw new InvalidContenttype("BackViewImg", "sekil tap gel");
                }

                if (book.BackViewImg.Length > 1048576)
                {
                    throw new InvalidImgSize("BackViewImg", "1 mbdan az yukle");
                }
              
                string newFileName = Helper.GetFileName(pathh, "upload", book.BackViewImg);
                BookImage bookImage = new BookImage
                {
                    book = book,
                    ImageUrl = newFileName,
                    IsPoster = false,
                };
                existBook.BookImages.Add(bookImage);
            };



            foreach (var item in existBook.BookImages.Where(x => !book.BookImagesIds.Contains(x.Id) && x.IsPoster == null))
            {
                string pathh = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                string path = Path.Combine(pathh, "upload", item.ImageUrl);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            existBook.BookImages.RemoveAll(x => !book.BookImagesIds.Contains(x.Id) && x.IsPoster == null);

            if (book.BookDetailImgs != null)
            {
                foreach (var img in book.BookDetailImgs)
                {

                    if (img.ContentType != "images/png" && img.ContentType != "image/jpeg")
                    {
                        throw new InvalidContenttype("BookDetailImgs", "get sekil yukle ");
                    }

                    if (img.Length > 1048576)
                    {
                        throw new InvalidImgSize("BookDetailImgs", "1 mbdan az yukle");
                    }
                    string path = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                    string newFileName = Helper.GetFileName(path, "upload", img);
                    BookImage bookImage = new BookImage
                    {
                        book = book,
                        ImageUrl = newFileName,
                        IsPoster = null,
                    };
                    existBook.BookImages.Add(bookImage);
                }
            }


            existBook.Name = book.Name;
            existBook.Description = book.Description;
            existBook.Tax = book.Tax;
            existBook.Code = book.Code;
            existBook.SalePrice = book.SalePrice;
            existBook.CostPrice = book.CostPrice;
            existBook.IsAvailable = book.IsAvailable;
            existBook.isBestseller = book.isBestseller;
            existBook.isNew = book.isNew;
            existBook.isFeatured = book.isFeatured;
            existBook.DisPrice = book.DisPrice;
            existBook.AuthorId = book.AuthorId;
            existBook.GenreId = book.GenreId;


            await _bookRepository.CommitAsync();
        }
    }
}
