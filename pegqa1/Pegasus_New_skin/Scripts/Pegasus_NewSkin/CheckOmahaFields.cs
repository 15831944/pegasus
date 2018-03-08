using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegasus_New_skin.Scripts.Pegasus_NewSkin
{
    class CheckOmahaFields
    {
        public void checkOmaha()
        {
            int i;

            String[] omahaFieldsSource;
            String[] omahaFieldsCurrentDictionary;

            omahaFieldsSource = System.IO.File.ReadAllLines(@"C:\Users\kshakir\Desktop\OmahaFieldsSource.txt");

            omahaFieldsCurrentDictionary = System.IO.File.ReadAllLines(@"C:\Users\kshakir\Desktop\OmahaFieldsSource.txt");

            for (i = 0; i < omahaFieldsSource.Length - 1; i++)
            {
               if (omahaFieldsSource[i] == omahaFieldsCurrentDictionary[i])
                {
                    Console.WriteLine("Field :" + i + " is present." );
                } else
                {
                    Console.WriteLine("Field :" + i + " isn't present.");
                }
            }

        }
    }
}
