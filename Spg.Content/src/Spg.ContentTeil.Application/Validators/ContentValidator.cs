using Spg.ContentTeil.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Search.Application.Validators
{
    public class ContentValidator
    {
        public bool Validate(Content content)
        {
            if (content == null)
            {
                Console.WriteLine("Content is null.");
                return false;
            }

            if (content.text == null)
            {
                Console.WriteLine("Content.text is null.");
                return false;
            }

            return true;
        }
    }
}
