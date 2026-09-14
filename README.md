[README.txt](https://github.com/user-attachments/files/32210856/README.txt)
SnippetVault (Kişisel Kod Parçacığı Yöneticisi)

Geliştiricilerin sık kullandıkları kod parçacıklarını verimli bir şekilde organize etmelerini, kategorize etmelerini ve güvenle saklamalarını sağlayan masaüstü uygulaması.
Bu araç, temel kod bloklarınızı tek bir merkezi veritabanında tutarak eski projeler arasında kod arama zahmetini ortadan kaldırır.


Temel Özellikler

* **Kod Yönetimi:** Kod parçacıklarını (snippet) kolayca ekleyin, düzenleyin ve silin (CRUD işlemleri).
* **Kategorizasyon:** Hızlı erişim için kodları programlama dillerine veya özel etiketlere göre sınıflandırın.
* **Kalıcı Veri Depolama:** Tüm veriler, entegre SQLite veritabanı kullanılarak yerel olarak güvenle saklanır.
* **Arama İşlevi:** İhtiyaç duyduğunuz kod bloğunu arama çubuğu üzerinden anında bulun.
* **Kullanıcı Dostu Arayüz:** Hızlı gezinme için tasarlanmış temiz ve sade Windows Forms (WinForms) arayüzü.

Kullanılan Teknolojiler

* **Programlama Dili:** C#
* **Altyapı:** .NET / Windows Forms (WinForms)
* **Veritabanı:** SQLite
* **Geliştirme Ortamı (IDE):** Visual Studio 2022

Kurulum ve Kullanım

Projeyi kendi bilgisayarınızda yerel olarak çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/ErenUlger80/SnippetValut.git
   ```
2. Projeyi açın:
   İndirdiğiniz klasördeki `.sln` uzantılı çözüm dosyasını **Visual Studio 2022** ile açın.
3. Paketleri Yükleyin:
   SQLite için gerekli olan NuGet paketlerinin yüklendiğinden emin olun. (Solution Explorer üzerinden projeye sağ tıklayıp "Restore NuGet Packages" seçeneğini kullanabilirsiniz.)
4. Derleme ve Çalıştırma:
   Uygulamayı derlemek ve başlatmak için `F5` tuşuna basın veya Visual Studio'daki "Start" (Başlat) butonuna tıklayın.

Öğrenim Çıktıları

Bu proje, masaüstü uygulama mimarisini kavramak adına kapsamlı bir pratik süreci olmuştur. Proje geliştirme sürecindeki temel kazanımlar:
* C# masaüstü ortamında ilişkisel bir veritabanının (SQLite) başarılı bir şekilde entegre edilmesi.
* WinForms kullanıcı arayüzü üzerinden veri ekleme, okuma, güncelleme ve silme (CRUD) operasyonlarının yönetilmesi.
* Kodun sürdürülebilirliğini artırmak için akademik sunum standartlarında proje mimarisinin oluşturulması.

---
*Bu proje [Eren Ülger](https://github.com/ErenUlger80) tarafından geliştirilmiştir.*
