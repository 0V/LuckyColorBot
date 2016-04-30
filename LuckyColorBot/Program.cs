using System;
using System.IO;
using System.Text;
using CoreTweet;

namespace LuckyColorBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var m = new RandomColor();
            var color = m.GetRandomColor();
            var creator = new ImageCreator();

            // TODO: Input your keys.
            const string ck = "YOUR CONSUMER KEY";
            const string cs = "YOUR CONSUMER SECRET";
            const string at = "YOUR ACCESS TOKEN";
            const string ats = "YOUR ACCESS TOKEN SECRET";

            var image = creator.GetImage(color);
            var tmpName = Path.GetTempFileName();

            try
            {
                var token = Tokens.Create(ck, cs, at, ats);

                var sb = new StringBuilder();
                sb.Append(DateTime.Now.ToShortDateString());
                sb.Append("になりました。今日のラッキーカラーは16進数RGB表記で「# ");
                sb.Append(color.ToHex());
                sb.Append("」です。 #lucky_color");

                image.Save(tmpName);
                var result = token.Media.Upload(media => new FileInfo(tmpName));
                token.Statuses.Update(status => sb.ToString(), media_ids => result);
            }
            catch (Exception e)
            {
                File.AppendAllText("error.log", DateTime.Now + ":" + e.Message + "\n");
            }
            finally
            {
                File.Delete(tmpName);
            }
        }
    }
}