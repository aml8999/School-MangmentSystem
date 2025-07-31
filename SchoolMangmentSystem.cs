using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Advanced_School_Management_System__OOP___SOLID_.Students;
using static Advanced_School_Management_System__OOP___SOLID_.StudentService;

namespace Advanced_School_Management_System__OOP___SOLID_
{



    public class Students
    {

        public string FullName { get; set; }
        public int Age { get; set; }
        public string ID { get; set; }
        public string TelphoneNumper { get; set; }
        public string Address { get; set; }

        public enum Gender
        {
            Male, Female
        }
        public enum Department
        {
            ScientificSciences, ScientificMathematics, Humanities
        }

        public Gender gender { get; set; }
        public Department department { get; set; }

        public List<Cources> CourseRegisterted { get; set; } = new List<Cources>();


    }

    public class NoException   // Exception نمنع حدوث  
    {

       public static int ValidInt(string prompt,int?min=null,int?max=null)  //داله بتتاكد ان المستخدم ادخل رقم
       {
        int value;

            while (true)
            {

                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out value))
                {
                    if (min == null || max == null) {
                        return value;
                    }
                    else {

                        if (value>=min&&value<=max)
                        {
                            return value;
                        }
                        else
                        {
                            Console.WriteLine($"Plese enter Numper Between{min}:{max}");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Invalid input!!..Please enter a number.");
                }
            }
     
       }

       public static string NonEmptyString(string prompt)  //داله بتتاكد ان  النص ليس Null
       {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();

            } while (string.IsNullOrEmpty(input));

            return input;

       }

        public static string IsNumper_NonEmptyString(string prompt)
        {

            string input;
            bool isValid;
            do
            {

                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
                isValid = !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
                if (!isValid)
                {
                    Console.WriteLine("Please enter numbers only and do not leave the field blank...");
                }
            } while (!isValid);

           

            return input;

        }

    }

    public class StudentService
    {
        public void AddNewStudent(List<Students> StudentsList, List<Cources> CourseRegisterted) // Add New Student...
        {
            var student = new Students();


            student.FullName = NoException.NonEmptyString("Enter Your Name: ");

           
            student.Age = NoException.ValidInt("Enter Your Age: ");


            student.ID = NoException.IsNumper_NonEmptyString("Enter Your ID: ");


            student.TelphoneNumper = NoException.IsNumper_NonEmptyString("Enter Your TelphoneNumper: ");


            student.Address = NoException.NonEmptyString("Enter Your Address: ");

          
            student.gender = (Students.Gender)NoException.ValidInt("Enter Gender\n1. Male\n2. Female\n",1,2) - 1;


            student.department = (Students.Department)NoException.ValidInt("Enter Your DepartMent\n1. ScientificSciences\n2. ScientificMathematics\n3. Humanities\n",1,3);   

            StudentsList.Add(student);

            Console.WriteLine("Added successfully\n");

        }

        public void ShowStudentDetails(List<Students> students)
        {

            if (students.Count == 0)
            {
                Console.WriteLine("No students added yet.");
                return;
            }

            foreach (var student in students)
            {
                Console.WriteLine("-------- Student Information --------\n");
                Console.WriteLine($"Name: {student.FullName}");

                Console.WriteLine($"Age: {student.Age}");

                Console.WriteLine($"ID: {student.ID}");

                Console.WriteLine($"Phone: {student.TelphoneNumper}");

                Console.WriteLine($"Address: {student.Address}");

                Console.WriteLine($"Gender: {student.gender}");

                Console.WriteLine($"Department: {student.department}");

                Console.WriteLine("Registered Courses:");


                if (student.CourseRegisterted.Count > 0)
                {
                    foreach (var item in student.CourseRegisterted)
                    {
                        Console.WriteLine($"{item.CourseName}");
                    }
                }
                else
                {
                    Console.WriteLine("No courses registered.");
                }
            }

        }

    }
        interface EssentialForAllCourses // الinterface البيحتوى على المعلومات الاساسيهلكل كورس
        {

            string CourseName { get; set; }
            int Hours { get; set; }
            int MaxStudents { get; set; }
            int CurrentCount { get; set; }



        }


        public abstract class Cources : EssentialForAllCourses //  كوسيط  نقلل التكرار ونزود مرونه abstract class استخدمت  
        {


            public string CourseName { get; set; }
            public int Hours { get; set; }
            public int MaxStudents { get; set; }
            public int CurrentCount { get; set; }
            public string CourseCode { get; set; }
            public List<Department> AvailableDepartments { get; set; } = new List<Department>(); //من الاقسام المتاحه لكل كورس بحيث الكورس متاح يكون مفتوح لاكتر من قسم  list هنا عملت 
               // public abstract void ShowCourseInformation();
             // public abstract void CourseRegistration(Students students);
            public string Lecturer { get; set; }

            public int CoursePrise;


        }

        public class AddingCourse : Cources
        {

            public void addingCourse(List<Cources> AvilableCouces)

            { 

               this.CourseName = NoException.NonEmptyString("Enter Course Name: ");

               
               this.Hours = NoException.ValidInt("Enter Course Hours: ");

              
               this.CoursePrise = NoException.ValidInt("Enter Course price: ");

               this.MaxStudents = NoException.ValidInt("Enter The Maximum Numper of Student: ");

               this.Lecturer = NoException.NonEmptyString("Enter The Name Of Licture: ");

               this.CourseCode = NoException.NonEmptyString("Enter The CoursesCode: ");

                Console.WriteLine();

                
                Department department = (Department)NoException.ValidInt("Enter The Numper of Departments That Can Enroll\n1.ScientificSciences\n2.ScientificMathematics\n3.Humanities  ",1,3) - 1;
                Console.WriteLine();
                this.CurrentCount = 0;

                AvilableCouces.Add(this); //ضيف الكائن الحالى الانت واقف فيه دلوقتى ا

                Console.WriteLine($"Added successfully..\n");

            }

        }
        public class ShowCourseInfo : AddingCourse //اضافه كورس 
        {

            public void ShowCourseInformation(List<Cources> AvilableCouces)
            {

            Console.WriteLine();
                for (int i = 0; i < AvilableCouces.Count(); i++)
                {
                    Console.WriteLine($"CourseName: {AvilableCouces[i].CourseName}");
                    Console.WriteLine($"CouseCode: {AvilableCouces[i].CourseCode}");
                    Console.WriteLine($"Course Price: {AvilableCouces[i].CoursePrise:c}");

                    foreach (var dept in AvailableDepartments)//بحيث منعدلش على الداله لو حبينا نغير فى الاقسام المتاحهه
                    {
                        Console.Write($"_ {dept}\n");
                    }

                    Console.WriteLine($"Hours: {AvilableCouces[i].Hours}");
                    Console.WriteLine($"Lecturer : {AvilableCouces[i].Lecturer}");
                    Console.WriteLine($"Numper of students enrilled int course: {AvilableCouces[i].CurrentCount}");
                    Console.WriteLine($"Maximum numper of student in Course: {AvilableCouces[i].MaxStudents}\n");

                }

            }

        }
        public class Registration // التسجيل فى كورس 
        {

            public void CourseRegistration(List<Students> StudentsList, List<Cources> AvilableCouces, List<Cources> CourseRegisterted)    //التسجيل فى الكورس
            {

                if (AvilableCouces != null && AvilableCouces.Count > 0)
                {

                   
                    string id = NoException.IsNumper_NonEmptyString("Enter Your ID: ");
                    bool studentisfound = false;
                    int currnt_student = 0;
                    int current_course = 0;
                    for (int i = 0; i < StudentsList.Count(); i++)   //بنتاكد ان الطالب موجود فى النظام
                    {
                        if (StudentsList[i].ID == id)
                        {
                            studentisfound = true;
                            currnt_student = i;
                        }
                    }

                    if (studentisfound)
                    {
                          //بنتاكد من وجود الكورس
                        string code = NoException.NonEmptyString("Enter the CourseCode: ");
                        bool found_course = false;
                        for (int j = 0; j < AvilableCouces.Count(); j++)
                        {
                            if (code == AvilableCouces[j].CourseCode)
                                found_course = true;
                            current_course = j;

                        }

                        if (found_course)
                        {
                            if (AvilableCouces[current_course].CurrentCount < AvilableCouces[current_course].MaxStudents) //بنتاكد  ان  العدد لم يكتمل
                            {

                                StudentsList[currnt_student].CourseRegisterted.Add(AvilableCouces[current_course]); // بنضيف الكورس فى قائمه  كورسات الطالب
                                AvilableCouces[current_course].CurrentCount++;
                            }
                            else
                            {
                                Console.WriteLine($"{AvilableCouces[current_course].CourseName}has  Completed...\n");
                            }

                        }
                        else
                        {
                            Console.WriteLine($"{code} isnot found!!..,please Make sure the course code is correct...\n");
                        }


                    }
                    else
                    {
                        Console.WriteLine($"The ID you entered  {id} is not found in system!!..,Please login first.\n");

                    }

                }
                else
                {
                    Console.WriteLine("There are not avilable Courses;Please Add Course First.\n");
                }
                Console.WriteLine();
                Console.WriteLine("Registration completed successfully..\n");


            }

        }

    public class Lougout
    { 
    
    public void logout(List<Students> StudentsList, List<Cources> AvilableCouces, List<Cources> CourseRegisterted)

        {

            if (AvilableCouces != null && AvilableCouces.Count > 0)
            {

                string id = NoException.IsNumper_NonEmptyString("Enter Your ID: ");
                bool studentisfound = false;
                int currnt_student = 0;
                int current_course = 0;
                for (int i = 0; i < StudentsList.Count(); i++)   //بنتاكد ان الطالب موجود فى النظام
                {
                    if (StudentsList[i].ID == id)
                    {
                        studentisfound = true;
                        currnt_student = i;
                    }
                }

                if (studentisfound)
                {
                     //بنتاكد من وجود الكورس
                    string code = NoException.NonEmptyString("Enter the CourseCode: ");
                    bool found_course = false;
                    for (int j = 0; j < AvilableCouces.Count(); j++)
                    {
                        if (code == AvilableCouces[j].CourseCode)
                            found_course = true;
                        current_course = j;

                    }

                    if (found_course)
                    {
                        if (AvilableCouces[current_course].CurrentCount < AvilableCouces[current_course].MaxStudents) //بنتاكد  ان  العدد لم يكتمل
                        {

                            StudentsList[currnt_student].CourseRegisterted.RemoveAt(currnt_student); // بنحذف الكورس فى قائمه  كورسات الطالب
                            AvilableCouces[current_course].CurrentCount--;
                        }
                        else
                        {
                            Console.WriteLine($"{AvilableCouces[current_course].CourseName}has  Completed...\n");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"{code} isnot found!!..,please Make sure the course code is correct...\n");
                    }


                }
                else
                {
                    Console.WriteLine($"The ID:{id} is not found in system!!..,Please Login first..\n");

                }

            }
            else
            {
                Console.WriteLine("There are not avilable Courses;Please Add Course First.\n");
            }

            Console.WriteLine("Logging out completed successfully..\n");


        }
    

    
    }


    public class RemoveStudent
    {
        public void delete_student(List<Students> StudentsList)
        {

            
            string id = NoException.IsNumper_NonEmptyString("Enter Your ID: ");
         

            bool studentisfound = false;
            int currnt_student = 0;
          
            for (int i = 0; i < StudentsList.Count(); i++)   //بنتاكد ان الطالب موجود فى النظام
            {
                if (StudentsList[i].ID==id)
                {
                    studentisfound = true;
                    currnt_student = i;
                    
                }
            }
            if (studentisfound)
            {
                StudentsList[currnt_student] = null;
            }
            else
            {

             Console.WriteLine($"The ID:{id} is not found in system!!..,Please Login first..\n");

            }

            Console.WriteLine("Your profiled has removed successfuly...");
        }


    }



    class Program
    {

            static void Main(string[] args)
            {

            Console.ForegroundColor = ConsoleColor.Magenta;


            List<Students> StudentsList = new List<Students>();
            List<Cources> AvilableCouces = new List<Cources>();
            List<Cources> CourseRegisterted = new List<Cources>(); //لسته بلكورسات السجل الطالب فيها


            RemoveStudent removeStudent = new RemoveStudent();
            Lougout lougout = new Lougout();
            StudentService service = new StudentService();
            ShowCourseInfo showCourseInfo = new ShowCourseInfo();
            AddingCourse addcourse = new AddingCourse();
            Registration registration = new Registration();
            Students currentStudent=null;



                bool Exist = false;
                int Choice=0;
            bool vaild_choice;
            do
            {
                    Console.WriteLine("--- School Management System ---\n\n");
                    Console.WriteLine("*********MENU**********\n");
                    Console.Write("1. Add Student\r\n2. View Students Details\r\n3. Add Course\r\n4. ViewCourses&Details\r\n5. Course registration\r\n6. Logout From Registered Cources\r\n7. Delete Student From system\r\n8. Exit\r\nEnter your choice: ");

                //بنمنع حدوث Eceptonلو  تم ادخال قيمه خاطئه 

                do
                {
                    vaild_choice = false;
                    try
                    {
                        Choice = int.Parse(Console.ReadLine());
                        if (Choice >= 1 && Choice <= 8)
                            vaild_choice = true;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        Console.WriteLine($" Invalid input!!..Please Enter a Vaild Numper:");


                    }
                } while (!vaild_choice);



                switch (Choice)
                    {
                        case 1:

                        service.AddNewStudent(StudentsList, CourseRegisterted);
                            currentStudent = StudentsList.Last();

                            break;

                        case 2:

                        service.ShowStudentDetails(StudentsList);

                            break;

                        case 3:

                        addcourse.addingCourse(AvilableCouces);
                          
                        break;

                        case 4:
                        showCourseInfo.ShowCourseInformation(AvilableCouces);


                        break;

                        case 5:
                        registration.CourseRegistration(StudentsList, AvilableCouces, CourseRegisterted);
                        

                        break;

                        case 6:

                        lougout.logout(StudentsList, AvilableCouces, CourseRegisterted);
                        break;

                        case 7:

                        removeStudent.delete_student(StudentsList);
                        break;

                    case 8:

                        Exist = true;

                        break;

                    }


                } while (!Exist);


            }

        
    }
}
