using System;
using System.Collections.Generic;
using System.Linq;

namespace KonzolnaAplikacija
{
    class Program
    {
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
            Student[] croatianStudentsOnMultipleUniversities = universities.SelectMany(r => new University[] { r.Name, r.Students }).ToArray();
            //Student[] multiplestudents = universities.SelectMany(student => student.Students).Where(student => student.Jmbag.Count() > 1).ToArray();
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
                new Student ("David", jmbag: "003647182"),
                new Student ("Mihael", jmbag: "003745689"),
                new Student ("Mirta", jmbag: "003645213")
            };
            return list.ToArray();
        }

        static Student[] FESB()
        {
            var list = new List<Student>()
            {
                new Student ("Rafael", jmbag: "003589652"),
                new Student ("Marijana", jmbag: "004568712"),
                new Student ("Benjamin", jmbag: "003648954")
            };
            return list.ToArray();
        }

        static Student[] MedicinskiFakultet()
        {
            var list = new List<Student>()
            {
                new Student ("Ivana", jmbag: "003512342"),
                new Student ("Luka", jmbag: "004568962"),
                new Student ("Mihael", jmbag: "003745689")
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
