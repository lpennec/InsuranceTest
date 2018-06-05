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

            //Sort by SeqNum
            underwriters.OrderBy(o => o.SeqNum).ToList();

            //Total of writtenLine
            float writtenlinetotal = underwriters.Sum(item => item.WrittenLine);

            if (writtenlinetotal == TotalOrder)
            {
                foreach (var underwriter in underwriters)
                {
                    underwriter.SignedLine = underwriter.WrittenLine;
                }
                return underwriters;
            } else
            {
                float div = TotalOrder / writtenlinetotal;
                //Function to loop through underwriters to apply new coeff
                return SignedLineCalc(div, underwriters, TotalOrder, "");
            }
        }


        //Method to find new SignedLine
        private List<Underwriter> SignedLineCalc(float div, List<Underwriter> underwriters, float TotalOrder, string skip)
        {
            foreach (var underwriter in underwriters)
            {
                if(underwriter.Name == skip)
                {
                    continue;
                }

                //DEFINE NEW FACTOR (BALANCE BETWEEN WRITTENLINE AND SIGNEDLINE
                underwriter.SignedLine = div * underwriter.WrittenLine;
                if (underwriter.SignedLine < underwriter.MinLine)
                {
                    underwriter.SignedLine = underwriter.MinLine;
                    float newTotalOrder = underwriters.Sum(item => item.WrittenLine) - underwriter.WrittenLine;
                    float newwrittenlinetotal = TotalOrder - underwriter.SignedLine;
                    float newdiv = newwrittenlinetotal / newTotalOrder;
                    return SignedLineCalc(newdiv, underwriters, newTotalOrder, underwriter.Name);
                }
            }
            return underwriters;
        }
    }
}
