namespace bankamatik
{
    internal class Program
    {
        static void transfer(double balance)
        {
            string receiverID = " ";
            int amount = 0;
            while (true)
            {

                Console.WriteLine("IBAN Giriniz:");
                receiverID = Console.ReadLine().ToUpper();
                if (receiverID.StartsWith("TR") && receiverID.Length == 14)
                {
                    Console.WriteLine("Lütfen Göndermek İstediğiniz Tutarı Giriniz:");
                    amount = Convert.ToInt32(Console.ReadLine());
                    if (amount > 0 && balance >= amount)
                    {
                        balance -= amount;
                        Console.WriteLine("Transfer İşlemi Başarılı.");
                        break;
                    }
                    else if (amount > 0 && balance < amount)
                    {
                        Console.WriteLine("Bakiyeden Yüksek Değer Transfer Edilemez.");
                    }
                }
                else if (receiverID.Length != 14)
                {
                    Console.WriteLine("Eksik yada Yanlış IBAN girdiniz!");
                }
                else
                {
                    Console.WriteLine("IBAN \"TR\" ile Başlamalı");
                }
            }
        } 
        static void login(double balance,string password,bool controller)
        {


            int attempt = 3;
            while (attempt > 0)
            {
                Console.WriteLine("Şifre Giriniz:");
                string passwordInput = Console.ReadLine();
                attempt--;
                if (passwordInput == password)
                {
                    Console.WriteLine("Giriş Başarılı!");
                    break;
                }
                else if (attempt == 0)
                {
                    Console.WriteLine("Giriş Hakkınız Kalmadı...");
                    Console.WriteLine("Sistem kilitlendi");
                    Thread.Sleep(5000);
                    Console.WriteLine("Sistem açıldı");
                    attempt = 3;
                }

                else
                {
                    Console.WriteLine("Giriş Başarısız!!");
                    Console.WriteLine("Tekrar Deneyiniz.");
                }
            }
            if (controller) //şifre değiştirme ekranında hatalı giriş yapılırsa sistem kitlemek yerine anamenüye yönlendiriyor. ????
            {
                Console.WriteLine("3 Kez Hatalı Şifre Girdiniz!! \n Anamenüye Yönlendiriliyorsunuz.");
                menu(balance,password);
            }
        } 
        static void withdraw(double balance) // para çekimi
        {
        }
        static void deposit(double balance) // para yatırımı
        {

        }
        static void educationalPayments(double balance) // eğitim ödemeleri ( ARIZALI)
        {
            Console.WriteLine("Sistemdeki Arıza Nedeniyle Gerçekleştirilemiyor!!");
        }
        static void payments(double balance) // ödemeler
        {

        }
        static string changePassword(double balance,string password)
        {
            login(balance,password, true);
            Console.WriteLine("Yeni Şifreyi Giriniz:");
            password = Console.ReadLine();
            return password;
        }

        static void kartliIslem(double balance, string password)
        {
            login(balance,password,false);
            Console.WriteLine(" Para Çekmek İçin 1,\n Para Yatırmak İçin 2,\n Para Transferi İçin 3,\n Eğitim Ödemeleri İçin 4,\n Ödemeler için 5,\n Bilgi Güncellemek için 6 Tuşlayınız.");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    withdraw(balance);
                    break;
                case "2":
                    deposit(balance);
                    break;
                case "3":
                    transfer(balance);
                    break;
                case "4":
                    educationalPayments(balance);
                    break;
                case "5":
                    payments(balance);
                    break;
                case "6":
                    changePassword(balance,password);
                    break;
                default:
                    Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz.");
                    break;

            }
        }
        static void kartsizIslem() { }
        static void menu(double balance, string password)
        {
            Console.WriteLine("Kartlı İşlem için 1,");
            Console.WriteLine("Kartsız İşlem için 2 Tuşlayınız.");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    kartliIslem(balance, password);
                    break;
                case "2":
                    kartsizIslem();
                    break;
                default:
                    Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz.");
                    break;

            }
        }
        static void Main(string[] args)
        {
            double balance = 25000;
            string password = "ab18";
            menu(balance, password);
            //login();
            //transfer(balance);


        }
    }
}
