namespace bankamatik
{
    internal class Program
    {
        static void toMenu(double balance, string password, bool isCard)
        {
            Console.WriteLine("Ana Menüye Dönmek için 9\nÇıkmak için 0 Tuşlayınız.");
            string choice1 = Console.ReadLine();
            switch (choice1)
            {
                case "9":
                    mainMenu(balance, password, isCard);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz!!");
                    break;
            }
        }
        static double transferMethod(double balance)
        {

            int amount = 0;
            Console.WriteLine("Lütfen Göndermek İstediğiniz Tutarı Giriniz:");
            amount = Convert.ToInt32(Console.ReadLine());
            if (amount > 0 && balance >= amount)
            {
                balance -= amount;
                Console.WriteLine("Transfer İşlemi Başarılı.");
                Console.WriteLine("Kalan Bakiye:" + balance);
                //break;
            }
            else if (amount > 0 && balance < amount)
            {
                Console.WriteLine("Bakiyeden Yüksek Değer Transfer Edilemez.");
            }
            return balance;
        }
        static double transfer(double balance, int isEFT)
        {
            string receiverID = " ";
            while (true)
            {
                if (isEFT == 1)
                {
                    Console.WriteLine("IBAN Giriniz:");
                }
                else
                {
                    Console.WriteLine("Hesap Numarası Giriniz:");
                }

                receiverID = Console.ReadLine().ToUpper();
                if (receiverID.StartsWith("TR") && receiverID.Length == 14 && isEFT == 1) // iban ile transfer
                {
                    balance = transferMethod((balance));
                    break;
                }
                else if (receiverID.Length != 14 && isEFT == 1)
                {
                    Console.WriteLine("Eksik yada Yanlış IBAN girdiniz!"); // iban numarası eksik yazıldığında verilecek hata mesajı
                }
                else if (receiverID.StartsWith("TR") == false && isEFT == 1)
                {
                    Console.WriteLine("IBAN \"TR\" ile Başlamalı");// iban tr ile başlamazsa verilecek hata mesajı
                }
                else if (isEFT == 2 && receiverID.Length == 11)
                {
                    balance = transferMethod((balance));
                    break;
                }
                else if (receiverID.Length != 11 && isEFT == 2)
                {
                    Console.WriteLine("Eksik yada Yanlış Hesap Numarası girdiniz!"); // hesap numarası eksik yazıldığında verilecek hata mesajı
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
                        cardMenu(balance, password);
                    }
                    else
                    {
                        Console.WriteLine("Sistem kilitlendi");
                        Thread.Sleep(5000);
                        cardMenu(balance, password);
                    }
                }

                else
                {
                    Console.WriteLine("Giriş Başarısız!!");
                    Console.WriteLine("Tekrar Deneyiniz.");
                }
            }

        }
        static double withdraw(double balance, string password, bool isCard) // para çekimi
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
                    toMenu(balance, password, isCard);
                }
            }
            else
            {
                Console.WriteLine("Bakiye Yetersiz!!");
                toMenu(balance, password, isCard);
            }
            return balance;
        }
        static void withdrawCepbank(double balance, string password)
        {
            string citizenID = "";
            string phoneNumber = "";

            int attemt = 3;
            while (attemt > 0)
            {
                Console.WriteLine("Lütfen T.C Kimlik Numaranızı Giriniz.");
                citizenID = Console.ReadLine();
                Console.WriteLine("Lütfen Başında Sıfır Olmayacak Şekilde Telefon Numaranızı Giriniz.");
                phoneNumber = Console.ReadLine();
                if (phoneNumber.Length == 10 && citizenID.Length == 11)
                {
                    Console.WriteLine("Ödeme Başarılı");
                    toMenu(balance, password, false);
                }
                else if (phoneNumber.Length != 10)
                {
                    Console.WriteLine("Telefon Numarası Bulunamadı!!");
                    
                }
                else
                {
                    Console.WriteLine("T.C Kimlik Numarası 11 Haneli Olmalıdır!");
                    
                }
                attemt--;
                Console.WriteLine("nLütfen Tekrar Deneyiniz");
            }
        }
        static void depositToOwnAccount(double balance, string password, bool isCard) // hesaba para yatırımı
        {
            if (isCard == true)
            {
                Console.WriteLine("Yatırılan Tutarı Giriniz");
                double depositAmount = Convert.ToDouble(Console.ReadLine());
                if (depositAmount > 0)
                {
                    balance += depositAmount;
                    Console.WriteLine("Yeni Bakiyeniz:" + balance);
                    toMenu(balance, password, isCard);
                }
                else
                {
                    Console.WriteLine("Yatırılan Tutar Negatif Olamaz!");
                    toMenu(balance, password, isCard);

                }
            }
            else
            {
                string receiverID = "";
                while (true)
                {
                    Console.WriteLine("Hesap Numarası Giriniz:");
                    receiverID = Console.ReadLine();
                    if (receiverID.Length == 11)
                    {
                        Console.WriteLine("İşlem Başarılı");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Eksik yada Yanlış Hesap Numarası girdiniz!"); // hesap numarası eksik yazıldığında verilecek hata mesajı
                    }
                    toMenu(balance, password, isCard);
                }
            }
        }

        static void depositToCredit(double balance, string password, bool isCard) // karta para yatırımı
        {
            Console.WriteLine("Kredi Kartı Numarası Giriniz.");
            string cardNumber = Console.ReadLine();
            if (cardNumber.Length == 12)
            {
                Console.WriteLine("Ödeme Başarılı\nYeni Bakiye:" + balance);
                toMenu(balance, password, isCard);
            }
            else
            {
                Console.WriteLine("Kart Numarası 12 Haneli Olmalı!!");
                toMenu(balance, password, isCard);
            }
        }
        static void educationalPayments(double balance, string password, bool isCard) // eğitim ödemeleri ( ARIZALI)
        {
            Console.WriteLine("Sistemdeki Arıza Nedeniyle Gerçekleştirilemiyor!!");
            toMenu(balance, password, isCard);
        }
        static void payments(double balance, string password, bool isCard) // ödemeler
        {
            Console.WriteLine("Fatura Tutarı Giriniz:");
            double bill = Convert.ToDouble(Console.ReadLine());
            if (bill < balance)
            {
                balance -= bill;
                Console.WriteLine("Ödeme Başarılı\nYeni Bakiye:" + balance);
                toMenu(balance, password, isCard);
            }
            else
            {
                Console.WriteLine("Bakiye Yetersiz!!");
                toMenu(balance, password, isCard);
            }
        }
        static string changePassword(double balance, string password)
        {
            login(balance, password, true);
            Console.WriteLine("Yeni Şifreyi Giriniz:");
            password = Console.ReadLine();
            return password;
        }
        static void mainMenu(double balance, string password, bool isCard)
        {
            if (isCard == false)
            {
                Console.Write(" Cepbank");
            }

            Console.Write(" Para Çekmek İçin 1,\n Para Yatırmak İçin 2,\n Para Transferi İçin 3,\n Eğitim Ödemeleri İçin 4,\n Ödemeler için 5");
            if (isCard == true)
            {
                Console.WriteLine(",\n Bilgi Güncellemek için 6 Tuşlayınız.");
            }
            else
            {
                Console.WriteLine(" Tuşlayınız.");
            }

            string choice = Console.ReadLine();

            switch (choice)
            {

                case "1": //çekim
                    if (isCard)
                        balance = withdraw(balance, password, isCard);
                    else
                        withdrawCepbank(balance, password);
                    mainMenu(balance, password, isCard);

                    break;
                case "2": // yatırım
                    Console.WriteLine("Kredi Kartına Yatırmak İçin 1\nKendi Hesabınıza yatırmak için  2 Tuşlayınız.");
                    string value = Console.ReadLine();
                    switch (value)
                    {
                        case "1":

                            depositToCredit(balance, password, isCard);
                            break;
                        case "2":
                            depositToOwnAccount(balance, password, isCard);
                            break;
                        case "9":
                            mainMenu(balance, password, isCard);
                            break;
                        case "0":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz!");
                            break;
                    }

                    //mainMenu(balance, password, isCard);
                    break;
                case "3": // transfer
                    //transfer(balance);
                    Console.WriteLine("Başka Hesaba EFT için 1\nBaşka Hesaba Havale için 2 Tuşlayınız.");
                    int s = Convert.ToInt32(Console.ReadLine());
                    if (s == 1 || s == 2)// 1 eft 2 hesap numarası için
                        balance = transfer(balance, s);
                    else
                        Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz.");
                    mainMenu(balance, password, isCard);
                    break;
                case "4": // eğitim
                    educationalPayments(balance, password, isCard);
                    mainMenu(balance, password, isCard);
                    break;
                case "5": //diğer ödemeler
                    payments(balance, password, isCard);
                    mainMenu(balance, password, isCard);
                    break;
                case "6": // şifre değiştirme
                    if (isCard = true)
                    {
                        password = changePassword(balance, password);
                        mainMenu(balance, password, isCard);

                    }
                    else
                    {
                        Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz.");
                        mainMenu(balance, password, isCard);
                    }
                    break;

                default:
                    Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz.");
                    mainMenu(balance, password, isCard);
                    break;

            }
        }

        #region withCard
        /*static void withCard(double balance, string password,bool isCard)
        {
            if (control == false) // ilk girişten sonra tekrar şifre istememesi için
            {
                login(balance, password, false);
            }

            mainMenu(balance, password,true);

            Console.WriteLine(" Para Çekmek İçin 1,\n Para Yatırmak İçin 2,\n Para Transferi İçin 3,\n Eğitim Ödemeleri İçin 4,\n Ödemeler için 5,\n Bilgi Güncellemek için 6 Tuşlayınız.");

            string choice = Console.ReadLine();

            switch (choice)
            {

                case "1": //çekim

                    balance = withdraw(balance, password);
                    withCard(balance, password, true);

                    break;
                case "2": // yatırım
                    Console.WriteLine("Kredi Kartına Yatırmak İçin 1\nKendi Hesabınıza yatırmak için  2 Tuşlayınız.");
                    string value = Console.ReadLine();
                    switch (value)
                    {
                        case "1":
                            depositToCredit(balance, password);
                            break;
                        case "2":
                            depositToOwnAccount(balance, password);
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
                case "3": // transfer
                    //transfer(balance);
                    Console.WriteLine("Başka Hesaba EFT için 1\nBaşka Hesaba Havale için 2 Tuşlayınız.");
                    int s = Convert.ToInt32(Console.ReadLine());
                    if (s == 1 || s == 2)
                        balance = transfer(balance, s);
                    else
                        Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz.");
                    withCard(balance, password, true);
                    break;
                case "4": // eğitim
                    educationalPayments(balance, password);
                    withCard(balance, password, true);
                    break;
                case "5": //diğer ödemeler
                    payments(balance, password);
                    withCard(balance, password, true);
                    break;
                case "6": // şifre değiştirme
                    password = changePassword(balance, password);
                    withCard(balance, password, true);
                    break;
                default:
                    Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz.");
                    withCard(balance, password, true);
                    break;

            }
            
        }*/
        #endregion
        #region withoutCard
        //static void withoutCard(double balance, string password)
        //{
        //    mainMenu(balance, password,false);
        //}
        #endregion
        static void cardMenu(double balance, string password)
        {
            Console.WriteLine("Kartlı İşlem için 1,");
            Console.WriteLine("Kartsız İşlem için 2 Tuşlayınız.");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":

                    login(balance, password, false);
                    mainMenu(balance, password, true);
                    break;
                case "2":
                    mainMenu(balance, password, false);
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
            cardMenu(balance, password);
            //login();
            //transfer(balance);


        }
    }
}
