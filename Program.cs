using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace stega
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 3)
            {
                if (args[0] == "--hide")
                {
                    Hide(args[1], args[2]);
                }
                else if (args[0] == "--show")
                {
                    Console.WriteLine("Pro příkaz --show si zadal špatný počet argumentů");
                }
                else
                {
                    Console.WriteLine("Příkaz \"" + args[0] + "\" neznám");
                }

            }
            else if (args.Length == 2)
            {
                if (args[0] == "--show")
                {
                    Console.WriteLine("Obrázek obsahoval tuto zprávu:" + Show(args[1])); 
                }
                else if (args[0] == "--hide")
                {
                    Console.WriteLine("Pro příkaz --hide si zadal špatný počet argumentů");
                }
                else
                {
                    Console.WriteLine("Příkaz \"" + args[0] + "\" neznám");
                }
            }
            else
            {
                Console.WriteLine("Zadal jsi špatný počet atgumentů zkus to znova :-) ");
            }
            
           
        }

        static void Hide(string msg, string dir)
        {
            Bitmap image = new Bitmap(dir);
            Random ran = new Random();
            int x = 0;
            int y = 0;
            int digit = 0;
            int posun = ran.Next(256);
            Color cp = image.GetPixel(image.Width-1, image.Height-1);
            Color cp1;
            Color cp2;
            image.SetPixel(image.Width - 1, image.Height - 1, Color.FromArgb(cp.A, cp.R, posun, msg.Length));
            foreach (char ch in msg)
            {
                for (int i = 0; i < 3; i++)
                {
                    int ich = Convert.ToInt32(ch);
                    if (i == 0)
                    {
                        digit = (ich - ich % 100)/100;
                        
                    }
                    if (i == 1)
                    {
                        digit = (ich % 100 - ich % 10) / 10;
                        
                    }
                    if (i == 2)
                    {
                        digit = ich % 10;
                        
                    }
                    cp = image.GetPixel(x, y);
                    if (x>0)
                    {
                        cp1 = image.GetPixel(x - 1, y);
                    }
                    else
                    {
                        cp1 = image.GetPixel(x, y + 1);
                    }
                    if (x < image.Width - 2)
                    {
                        cp2 = image.GetPixel(x + 1, y);
                    }
                    else
                    {
                        cp2 = image.GetPixel(x, y + 1);
                    }

                    image.SetPixel(x, y, Color.FromArgb(cp.A,(cp1.R + cp2.R)/2 + digit, cp.G, cp.B));
                    if (x > image.Width-posun)
                    {
                        x = 0;
                        y++;
                    }
                    else
                    {
                        x+=posun;
                    }

                }


            }
            image.Save(dir.Split('.')[0]+"_encoded.bmp");
        }

        static string Show(string dir)
        {
            Bitmap image = new Bitmap(dir);
            Color cp = image.GetPixel(image.Width - 1, image.Height - 1);
            Color cp1;
            Color cp2;
            int lenght = cp.B;
            int posun = cp.G;
            int x = 0;
            int y = 0;
            int ich;
            int digit;
            string msg = "";
            for (int i = 0; i < lenght; i++)
            {
                ich = 0;
                for (int a = 0; a < 3; a++)
                {
                    cp = image.GetPixel(x, y);
                    digit = Convert.ToChar(cp.R);
                    if (x > 0)
                    {
                        cp1 = image.GetPixel(x - 1, y);
                    }
                    else
                    {
                        cp1 = image.GetPixel(x, y + 1);
                    }
                    if (x < image.Width - 2)
                    {
                        cp2 = image.GetPixel(x + 1, y);
                    }
                    else
                    {
                        cp2 = image.GetPixel(x, y + 1);
                    }
                    int av = (cp1.R + cp2.R) / 2;
                    if (a == 0)
                    {
                        ich += (digit - av) * 100;
                    }
                    if (a == 1)
                    {
                        ich += (digit - av) * 10;
                    }
                    if (a == 2)
                    {
                        ich += (digit - av);
                    }
                    
                    if (x > image.Width - posun)
                    {
                        x = 0;
                        y++;
                    }
                    else
                    {
                        x+=posun;
                    }
                    
                } 
                msg += Convert.ToChar(ich);                
            }
            return msg;
        }
    }
}
