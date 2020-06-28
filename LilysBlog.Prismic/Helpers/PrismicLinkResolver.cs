using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prismic;
using prismic.fragments;

namespace LilysBlog.Prismic.Helpers
{
    public class PrismicLinkResolver : DocumentLinkResolver
    {
        public override string Resolve(DocumentLink link)
        {
            if(link.Type == "shelf")
            {
                return $"/shelves/{link.Uid}";
            }
            else if (link.Type == "experiment")
            {
                return $"/experiments/{link.Uid}";
            }

            return null;
        }
    }
}
