using System;

namespace Diary.Api
{
    public class Diary
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string SharedWith { get; set; }
    }
}

    
