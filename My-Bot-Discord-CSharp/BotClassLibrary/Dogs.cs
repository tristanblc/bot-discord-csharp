namespace BotClassLibrary
{
    public class Dogs : Animal
    {

        public int fileSizeByte { get; private set; }        

        public Dogs(int fileSizeByte, string url) : base(url)
        {
            this.fileSizeByte = fileSizeByte;
           

        }

    }
}