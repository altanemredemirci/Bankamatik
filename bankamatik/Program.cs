namespace bankamatik
{
    internal class Program
    {
        static void toMenu(double balance,string password)
        {
            Console.WriteLine("Ana Menüye Dönmek için 9\nÇıkmak için 0 Tuşlayınız.");
            string choice1 = Console.ReadLine();
            switch (choice1)
            {
                case "9":
                    withCard(balance,password,true);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz!!");
                    break;
            }
        }
        static double transfer(double balance)
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
                        Console.WriteLine("Kalan Bakiye:" + balance);
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
            return balance;
        }
        static void login(double balance, string password, bool controller)
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
                    if (controller) //şifre değiştirme ekranında hatalı giriş yapılırsa sistem kitlemek yerine anamenüye yönlendiriyor. ????
                    {
                        Console.WriteLine("3 Kez Hatalı Şifre Girdiniz!! \n Anamenüye Yönlendiriliyorsunuz.");
                        menu(balance, password);
                    }
                    else
                    {
                        Console.WriteLine("Sistem kilitlendi");
                        Thread.Sleep(5000);
                        menu(balance, password);
                    }
                }

                else
                {
                    Console.WriteLine("Giriş Başarısız!!");
                    Console.WriteLine("Tekrar Deneyiniz.");
                }
            }
            
        }
        static double withdraw(double balance,string password) // para çekimi
        {
            if (balance > 0)
            {
                Console.WriteLine("Çekilecek Tutarı Giriniz:");
                double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                if (withdrawAmount <= balance)
                {
                    balance -= withdrawAmount;
                    Console.WriteLine("İşlem Başarılı\n Anamenüye Yönlendiriliyorsunuz.");
                    Console.WriteLine("Kalan Bakiye:" + balance);
                }
                else
                {
                    Console.WriteLine("Bakiye Yetersiz!!");
                    toMenu(balance,password);
                }
            }
            else
            {
                Console.WriteLine("Bakiye Yetersiz!!");
                toMenu(balance, password);
            }
            return balance;
        }
        static void depositToOwnAccount(double balance, string password) // para yatırımı
        {
            Console.WriteLine("Yatırılan Tutarı Giriniz");
            double depositAmount = Convert.ToDouble(Console.ReadLine());
            if (depositAmount < 0)
            {
                balance += depositAmount;
                Console.WriteLine("Yeni Bakiyeniz:"+balance);
                toMenu(balance, password);
            }
            else
            {
                Console.WriteLine("Yatırılan Tutar Negatif Olamaz!");
                toMenu(balance, password);
                
            }
        }

        static void depositToCredit(double balance, string password)
        {
            Console.WriteLine("Kredi Kartı Numarası Giriniz.");
            string cardNumber = Console.ReadLine();
            if (cardNumber.Length == 12)
            {
                Console.WriteLine("Ödeme Başarılı");
                toMenu(balance, password);

            }
            else
            {
                Console.WriteLine("Kart Numarası 12 Haneli Olmalı!!");
                toMenu(balance, password);
            }
        }
        static void educationalPayments(double balance) // eğitim ödemeleri ( ARIZALI)
        {
            Console.WriteLine("Sistemdeki Arıza Nedeniyle Gerçekleştirilemiyor!!");
        }
        static void payments(double balance) // ödemeler
        {

        }
        static string changePassword(double balance, string password)
        {
            login(balance, password, true);
            Console.WriteLine("Yeni Şifreyi Giriniz:");
            password = Console.ReadLine();
            return password;
        }

        static void withCard(double balance, string password, bool control)
        {
            if (control == false)
            {
                login(balance, password, false);
            }
            Console.WriteLine(" Para Çekmek İçin 1,\n Para Yatırmak İçin 2,\n Para Transferi İçin 3,\n Eğitim Ödemeleri İçin 4,\n Ödemeler için 5,\n Bilgi Güncellemek için 6 Tuşlayınız.");

            string choice = Console.ReadLine();

            switch (choice)
            {

                case "1":

                    balance = withdraw(balance,password);
                    withCard(balance, password, true);

                    break;
                case "2":
                    Console.WriteLine("Kredi Kartına Yatırmak İçin 1\nKendi Hesabınıza yatırmak için  2 Tuşlayınız.");
                    string value = Console.ReadLine();
                    switch (value)
                    {
                        case "1":
                            depositToCredit(balance,password);
                            break;
                        case "2":
                            depositToOwnAccount(balance,password);
                            break;
                        case "9":
                            withCard(balance, password, true);
                            break;
                        case "0":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz!");
                            break;
                    }

                    withCard(balance, password, true);
                    break;
                case "3":
                    //transfer(balance);
                    balance = transfer(balance);
                    withCard(balance, password, true);
                    break;
                case "4":
                    educationalPayments(balance);
                    withCard(balance, password, true);
                    break;
                case "5":
                    payments(balance);
                    withCard(balance, password, true);
                    break;
                case "6":
                    changePassword(balance, password);
                    withCard(balance, password, true);
                    break;
                default:
                    Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz.");
                    withCard(balance, password, true);
                    break;

            }
        }
        static void withoutCard(double balance, string password)
        {
            menu(balance, password);
        }
        static void menu(double balance, string password)
        {
            Console.WriteLine("Kartlı İşlem için 1,");
            Console.WriteLine("Kartsız İşlem için 2 Tuşlayınız.");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    withCard(balance, password, false);
                    break;
                case "2":
                    withoutCard(balance, password);
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
