using Discord;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new DiscordClient();
            bot.MessageReceived += Bot_MessageReceived;

            bot.ExecuteAndWait(async () =>
           {
               await bot.Connect("MTY4NjU2Nzk1MDYwMDExMDA4.Ce4r7w.4q8AJKHXRQB_lCPSHXBwOrUYFDM");
               bot.SetGame("with a kitty");
               var avatar = Image.FromFile("images/misaka_cat.png");
               var stream = new System.IO.MemoryStream();
               avatar.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
               await bot.CurrentUser.Edit(avatar: stream,avatarType: ImageType.Png);
             
           });
            
        }

        private static void Bot_MessageReceived(object sender, MessageEventArgs e)
        {
            string text = e.Message.Text;

            if (e.Message.IsAuthor) return;
            if(text == "!rules")
            {
                e.Channel.SendMessage(e.User.Mention + " \"There are none\", Misaka-0170 blankly states.");
            }

            if (text.ToLower().Contains("misaka") )
            {
                if (text.ToLower().Contains("who") || text.ToLower().Contains("what"))
                {

                    e.Channel.SendMessage(e.User.Mention + " \"Misaka is Misaka\", Misaka-0170 reminds you.");
                    e.Channel.SendFile("images/misaka_cat.jpg");
                }
            }

           
           
        }

    }
}
