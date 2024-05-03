Console.WriteLine("Lab 10 (variant 7)");
Console.Write("Enter the number of task (1 or 2): ");
int choice = Int32.Parse(Console.ReadLine());
switch (choice){
    case 1:{
        Task1 lab8task1 = new Task1();
        lab8task1.Run();
    }break;
    case 2:{
        Task2 lab8task2 = new Task2();
        lab8task2.Run();
    }break;
}

class Task1{
    class WorkerException : Exception{
        public WorkerException(string message) : base(message) { }
    }

    class ArrayTypeMismatchException : WorkerException{
        public ArrayTypeMismatchException(string message) : base(message) { }
    }

    class DivideByZeroException : WorkerException{
        public DivideByZeroException(string message) : base(message) { }
    }

    class IndexOutOfRangeException : WorkerException{
        public IndexOutOfRangeException(string message) : base(message) { }
    }

    class InvalidCastException : WorkerException{
        public InvalidCastException(string message) : base(message) { }
    }

    class OutOfMemoryException : WorkerException{
        public OutOfMemoryException(string message) : base(message) { }
    }

    class OverflowException : WorkerException{
        public OverflowException(string message) : base(message) { }
    }

    class StackOverflowException : WorkerException{
        public StackOverflowException(string message) : base(message) { }
    }

    class Worker{
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        public override string ToString()
        {
            return $"{LastName}, {FirstName}, {MiddleName}, {Gender}, {Age}, {Salary}";
        }
    }

    public void Run(){
        try{
            List<Worker> workers = new List<Worker>();
            string file = "C:\\Users\\User\\github-classroom\\csharplab10-KaliuzhnaMariia\\Lab9_10CharpT\\workers.txt";
            using (StreamReader sr = new StreamReader(file)){
                while (!sr.EndOfStream){
                    string[] data = sr.ReadLine().Split(',');
                    if (data.Length == 6){
                        Worker work = new Worker{
                            LastName = data[0],
                            FirstName = data[1],
                            MiddleName = data[2],
                            Gender = data[3],
                            Age = int.Parse(data[4]),
                            Salary = double.Parse(data[5])
                        };
                        workers.Add(work);
                    }
                    else{
                        Console.WriteLine($"Incorrect data format for the worker: {string.Join(",", data)}");
                    }
                }
            }

            Queue<Worker> lowSalaryWorkers = new Queue<Worker>();
            Queue<Worker> highSalaryWorkers = new Queue<Worker>();
            foreach (Worker work in workers){
                if (work.Salary < 10000)
                    lowSalaryWorkers.Enqueue(work);
                else
                    highSalaryWorkers.Enqueue(work);
            }

            Console.WriteLine("Workers with low salary (< 10000):");
            while (lowSalaryWorkers.Count > 0){
                Console.WriteLine(lowSalaryWorkers.Dequeue());
            }

            Console.WriteLine("\nEmployees with high salary ()>= 10000):");
            while (highSalaryWorkers.Count > 0){
                Console.WriteLine(highSalaryWorkers.Dequeue());
            }
        }
        catch (ArrayTypeMismatchException ex){
            Console.WriteLine($"ArrayTypeMismatchException: {ex.Message}");
        }
        catch (DivideByZeroException ex){
            Console.WriteLine($"DivideByZeroException: {ex.Message}");
        }
        catch (IndexOutOfRangeException ex){
            Console.WriteLine($"IndexOutOfRangeException: {ex.Message}");
        }
        catch (InvalidCastException ex){
            Console.WriteLine($"InvalidCastException: {ex.Message}");
        }
        catch (OutOfMemoryException ex){
            Console.WriteLine($"OutOfMemoryException: {ex.Message}");
        }
        catch (OverflowException ex){
            Console.WriteLine($"OverflowException: {ex.Message}");
        }
        catch (StackOverflowException ex){
            Console.WriteLine($"StackOverflowException: {ex.Message}");
        }  
    }
}

class Task2{
    public delegate void StudentEventHandler(object sender, StudentEventArgs e);

    public class Student{
        public event StudentEventHandler StudyStarted;
        public event StudentEventHandler StudyFinished;
        public event StudentEventHandler LectureAttended;
        public event StudentEventHandler PartyAttended;
        public event StudentEventHandler ExamPassed;

        private bool isStudying;

        public Student(){
            isStudying = false;
        }

        public void StartStudying(){
            if (!isStudying){
                Console.WriteLine("Студент почав навчання");
                OnStudyStarted(new StudentEventArgs("Студент почав навчання"));
                isStudying = true;
            }
            else{
                Console.WriteLine("Студент навчається");
            }
        }

        public void FinishStudying(){
            if (isStudying){
                Console.WriteLine("Студент закінчив навчання");
                OnStudyFinished(new StudentEventArgs("Студент закінчив навчання"));
                isStudying = false;
            }
            else{
                Console.WriteLine("Студент не навчається");
            }
        }

        public void AttendLecture(string lectureName){
            Console.WriteLine($"Студент відвідав лекцію: {lectureName}");
            OnLectureAttended(new StudentEventArgs($"Студент відвідав лекцію: {lectureName}"));
        }

        public void AttendParty(){
            if(!isStudying){
                Console.WriteLine($"Студент відвідав вечірку");
                OnPartyAttended(new StudentEventArgs($"Студент відвідав вечірку"));
            }
            else{
                Console.WriteLine("Студент навчається, а не ходить по вечіркам!");
            }
            
        }

        public void PassExam(string examName){
            Console.WriteLine($"Студент склав іспит: {examName}");
            OnExamPassed(new StudentEventArgs($"Студент склав іспит: {examName}"));
        }

        protected virtual void OnStudyStarted(StudentEventArgs e){
            StudyStarted?.Invoke(this, e);
        }

        protected virtual void OnStudyFinished(StudentEventArgs e){
            StudyFinished?.Invoke(this, e);
        }

        protected virtual void OnLectureAttended(StudentEventArgs e){
            LectureAttended?.Invoke(this, e);
        }

        protected virtual void OnPartyAttended(StudentEventArgs e){
            PartyAttended?.Invoke(this, e);
        }

        protected virtual void OnExamPassed(StudentEventArgs e){
            ExamPassed?.Invoke(this, e);
        }
    }

    public class StudentEventArgs : EventArgs{
        public string Message { get; }

        public StudentEventArgs(string message){
            Message = message;
        }
    }
    public void Run(){
        Student student = new Student();

        student.StudyStarted += Student_StudyStarted;
        student.StudyFinished += Student_StudyFinished;
        student.LectureAttended += Student_LectureAttended;
        student.PartyAttended += Student_PartyAttended;
        student.ExamPassed += Student_ExamPassed;

        bool isStudying = false;

        while (true){
            Console.WriteLine("\n1. Початок навчання");
            Console.WriteLine("2. Закінчення навчання");
            Console.WriteLine("3. Відвідав лекцію");
            Console.WriteLine("4. Відвідав вечірку");
            Console.WriteLine("5. Склав іспит");
            Console.WriteLine("6. Вихід\n");

            if (!int.TryParse(Console.ReadLine(), out int choice)){
                Console.WriteLine("Incorrect input. Please try again");
                continue;
            }

            switch (choice){
                case 1:{
                    if (!isStudying){
                        student.StartStudying();
                        isStudying = true;
                    }
                    else{
                        Console.WriteLine("Студент зараз навчається");
                    }
                }break;
                case 2:{
                    if (isStudying){
                        student.FinishStudying();
                        isStudying = false;
                    }
                    else{
                        Console.WriteLine("Студент не навчається");
                    }
                }break;
                case 3:{
                    Console.WriteLine("Введіть назву лекції: ");
                    string lectureName = Console.ReadLine();
                    student.AttendLecture(lectureName);                        
                    }break;
                case 4:{
                    student.AttendParty();
                }break;
                case 5:{
                    Console.WriteLine("Введіть назву іспиту: ");
                    string examName = Console.ReadLine();
                    student.PassExam(examName);
                }break;
                case 6:{
                    Environment.Exit(0);
                }break;
                default:{
                    Console.WriteLine("Error");
                            break;
                }
            }
        }
    }

    private static void Student_StudyStarted(object sender, StudentEventArgs e){
        Console.WriteLine(e.Message);
    }
    private static void Student_StudyFinished(object sender, StudentEventArgs e){
        Console.WriteLine(e.Message);
    }
    private static void Student_LectureAttended(object sender, StudentEventArgs e){
        Console.WriteLine(e.Message);
    }
    private static void Student_PartyAttended(object sender, StudentEventArgs e){
        Console.WriteLine(e.Message);
    }
    private static void Student_ExamPassed(object sender, StudentEventArgs e){
        Console.WriteLine(e.Message);
    }
}