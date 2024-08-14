using System;
namespace encsub {
    class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length > 0 && Path.GetExtension(args[0]) ==".srt")
            {
                byte fps;
                Console.Write("Selected file: "+Path.GetFileName(args[0])+"\n\nSelect framerate of your video (this affects the frame time when captions appears and disappears)\nFPS: ");
                while(true){
                    if(Byte.TryParse(Console.ReadLine(),out fps)){
                    //Console.WriteLine("Selected fps: "+fps);
                    break;
                }else  Console.WriteLine("Number is too big\nFPS: ");
                }
                Console.WriteLine("Converting file...");

           int srtlength()
            {
                using (StreamReader r = new StreamReader(args[0]))
                {
                    int n = 0;
                    string temp1 = r.ReadLine();
                    bool isNumeric = int.TryParse(temp1, out n);
                    int highest = 0;
                    while (temp1 != null)
                    {
                        isNumeric = int.TryParse(temp1, out n);
                        if (n > highest)
                        {
                            highest = n;
                        }
                        temp1 = r.ReadLine();
                    }
                    return highest;
                }
            }
            int line = 0;
            int border = srtlength();
            string output = "", temp = "";
            StreamReader srtfile = new StreamReader(args[0]);
            StreamWriter txtfile = new StreamWriter("script_"+Path.GetFileNameWithoutExtension(args[0])+".txt");
            while (line < border)
            {
                temp = srtfile.ReadLine();
                line++;
                output += temp + " ";
                temp = srtfile.ReadLine();
                output += temp.Substring(0, 8).Replace(':', ';') + ';';
                output += Convert.ToInt16(temp.Substring(9, 3)) * fps / 1000 + " ";
                output += temp.Substring(17, 8).Replace(':', ';') + ';';
                output += Convert.ToInt16(temp.Substring(26, 3)) * fps / 1000 + " ";
                output += srtfile.ReadLine();
                txtfile.WriteLine(output);
                output = "";
                temp = srtfile.ReadLine();

                if (temp.Length != 0)
                {
                    output += temp;
                    txtfile.WriteLine(output);
                    temp = srtfile.ReadLine();
                }
                output = "";
                if (temp!=null){
                    if (temp.Length != 0)
                {
                    output += temp;
                    txtfile.WriteLine(output);
                    temp = srtfile.ReadLine();
                }
                }
                output = "";
            }
                txtfile.Close();
                srtfile.Close();
                Console.WriteLine("Done!");
                Console.ReadLine();
            }
            else Console.WriteLine("Drag and drop SRT file on EXE, don't double click it or some other stuff idk"); 
            Console.ReadLine();
        }
    }
}