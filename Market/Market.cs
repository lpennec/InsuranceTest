using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Market
{
    public class Market
    {
        public List<Underwriter> MarketSigndown(float TotalOrder, List<Underwriter> underwriters)
        {
        // Sequel - Development Team interview
        // -----------------------------------
        // Please enter your code below this line

        
            //define new coefficient to share between underwriters
            underwriters = underwriters.OrderBy(x => x.SeqNum).ToList();
        restart:
            float coeff = (TotalOrder - underwriters.Sum(x => x.SignedLine)) / underwriters.Where(x => x.SignedLine == 0).Sum(x => x.WrittenLine);
            foreach (var underwriter in underwriters)
            {
                //if it has already signed don't change anything
                if (underwriter.SignedLine != 0)
                {
                    continue;
                }
                //if the anticipated Signedline is below Minline
                if (underwriter.WrittenLine * coeff < underwriter.MinLine)
                {
                    //Erase all SignedLine if SeqNum is lower and commit this one
                    foreach (var item in underwriters.Where(x => x.SeqNum < underwriter.SeqNum))
                    {
                        item.SignedLine = 0;
                    } 
                    underwriter.SignedLine = underwriter.MinLine;
                    goto restart;
                } else
                {
                    //commit SignedLine using calculated coeff
                    underwriter.SignedLine = underwriter.WrittenLine * coeff;
                }
            }
            return underwriters;
        }       
    }
}
