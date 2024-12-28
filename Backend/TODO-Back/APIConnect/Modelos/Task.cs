namespace APIConnect.Modelos
{
    public class Task
    {
        public string Description { get; set; }
        public string Priority { get; set; }
        public string EndDate { get; set; }

    
    }
    public class Priority
    {
        public string Option { get; set; }
        public int IdTask { get; set; }
        public string Content { get; set; }
    }
}
