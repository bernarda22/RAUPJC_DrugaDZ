namespace KonzolnaAplikacija
{
    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }

        public University(string name, Student[] students)
        {
            Name = name;
            Students = students;
        }
    }
}
