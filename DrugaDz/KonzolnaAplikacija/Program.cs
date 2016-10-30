using System;
using System.Collections.Generic;
using System.Linq;

namespace KonzolnaAplikacija
{
    class Program { 
    
        static void Main(string[] args)
        {
            //Treći zadatak
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            string[] strings = integers.GroupBy(i => i).Select(x => "Broj " + x.Key + " se ponavlja " + x.Count() + " puta").ToArray();

            // Četvrti zadatak
            Example1();
            Example2();

            //Peti zadatak
            University[] universities = GetAllCroatianUniversities();

            Student[] allCroatianStudents = universities.SelectMany(student => student.Students).Distinct().ToArray();
            Student[] croatianStudentsOnMultipleUniversities = universities
                .SelectMany(student => student.Students)
                .GroupBy(student => student)
                .Where(studentGroup => studentGroup.Count() > 1 )
                .Select(studentGroup => studentGroup.First())
                .ToArray();

            Student[] multiplestudents = universities
                .Where(university => university.Students.Where(student => student.Gender == Gender.Female).Count() == 0)
                .SelectMany(university => university.Students)
                .Distinct()
                .ToArray();    

            Console.In.ReadLine();
            
        }

        private static University[] GetAllCroatianUniversities()
        {
            var list = new List<University>()
            {
                new University("FER", students: FER()),
                new University("FESB", students: FESB()),
                new University("Medicinski Fakultet", students: MedicinskiFakultet()),
            };
            return list.ToArray();
        }

        static Student[] FER()
        {
            var list = new List<Student>()
            {
                new Student ("David", jmbag: "003647182", gender: Gender.Male),
                new Student ("Mihael", jmbag: "003745689", gender: Gender.Female),
                new Student ("Mirta", jmbag: "003645213", gender: Gender.Female)
            };
            return list.ToArray();
        }

        static Student[] FESB()
        {
            var list = new List<Student>()
            {
                new Student ("Rafael", jmbag: "003589652", gender: Gender.Male),
                new Student ("Marijana", jmbag: "004568712", gender: Gender.Female),
                new Student ("Benjamin", jmbag: "003648954", gender: Gender.Male),
                new Student ("Mirta", jmbag: "003645213", gender: Gender.Female)
            };
            return list.ToArray();
        }

        static Student[] MedicinskiFakultet()
        {
            var list = new List<Student>()
            {
                new Student ("Luka", jmbag: "004568962", gender: Gender.Male),
                new Student ("Mihael", jmbag: "003745689", gender: Gender.Male),
                new Student ("Rafael", jmbag: "003589652", gender: Gender.Male)
            };
            return list.ToArray();
        }

        static void Example1()
        {
            var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 ")
            };
            var ivan = new Student(" Ivan ", jmbag: " 001234567 ");
            // false :(
            bool anyIvanExists = list.Any(s => s == ivan);
        }

        static void Example2()
        {
            var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 "),
                new Student (" Ivan ", jmbag :" 001234567 ")
            };
            // 2 :(
            var distinctStudents = list.Distinct().Count();            
        }
    }
}
