using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.Common.CommonClass
{
   public class generatepassword
    {

        //Genaratepassword
        public string generate_password()
        {
            string NewPassword = "";
            //This one tells you which characters are allowed in this new password

            string strNumbers = "";
            string strCaptialLetters = "";
            string strSmallLetters = "";
            string strSpecialChars = "";

            strNumbers = "1,2,3,4,5,6,7,8,9,0";
            strCaptialLetters += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            strSmallLetters += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            strSpecialChars += "~,!,@,#,$,%,^,&,*,+,?";

            char[] sep = { ',' };


            string[] Captials = strCaptialLetters.Split(sep);
            string[] Numbers = strNumbers.Split(sep);
            string[] Small = strSmallLetters.Split(sep);
            string[] Special = strSpecialChars.Split(sep);

            //Random Numbers
            NewPassword += Generate(Numbers);

            //Random Captials
            NewPassword += Generate(Captials);

            //Random Special
            NewPassword += Generate(Special);

            //Random Small
            NewPassword += Generate(Small);

            return NewPassword;



        }
        //Genarate Methode
        public static string Generate(string[] Array)
        {
            string temp = "";
            string IDString = "";
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                temp = Array[rand.Next(0, Array.Length)];
                IDString += temp;
            }
            return IDString;
        }

    }
}
