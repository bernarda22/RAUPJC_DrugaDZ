namespace KonzolnaAplikacija
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Student;

            if (item == null)
            {
                return false;
            }

            return Jmbag.Equals(item.Jmbag) && Name.Equals(item.Name) && Gender.Equals(item.Gender);
        }

        public override int GetHashCode()
        {
            return Jmbag.GetHashCode();
        }

        public static bool operator ==(Student a, Student b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Jmbag == b.Jmbag && a.Gender == b.Gender && a.Name == b.Name;
        }

        public static bool operator !=(Student a, Student b)
        {
            return !(a == b);
        }
    }

    public enum Gender
    {
        Male, Female
    }
}