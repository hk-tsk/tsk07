using DACommon;
using DACommon.Entities;
using System.Collections.Generic;

namespace DA
{
    public class ContentRepository : BaseRepository<Content>
    {
        public ContentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Content> GetContent(string courseName, string category)
        {

            if (courseName == "React")
            {
                if (category == "CreateProject")
                {
                    var data = new List<Content>(){

                       new Content()
                       {
                           ContentText="ایحاد تینسییت بنیتسب سبت بینبتیس بیسب یسنبتیسب نبتسیینبتیب نبت یسبنتیس بتب سینبت یسنبتیسنمبتنسمب س",
                           ContentType="Paragraph"
                       }
                       ,
                        new Content()
                       {
                           ContentText=@"public IEnumerable<Category> Get(string name)
    {
        return categoryBl.GetCategories(name);
    }",
                           ContentType="Code"
                       }
                       , new Content()
                       {
                           ContentText="خصهقهئثزثقث ت  ث  ثفقثت  فف ف  فق فق فق فثقفثق ثقف",
                           ContentType="Paragraph"
                       }
                    };

                    return data;
                }
                else
                {
                    var data = new List<Content>(){

                       new Content()
                       {
                           ContentText="pgg g ppopo oo oo oo ooo oo oo oo oo o o pp ppp pp p",
                           ContentType="Paragraph"
                       }
                    };

                    return data;
                }

            }
            else
            {
                var data = new List<Content>(){

               new Content()
               {
                   ContentText="ww ww  w  w wwwwww www  www  ww  ww ww  ",
                    ContentType="Paragraph"
               }
            };

                return data;
            }

        }
    }
}
