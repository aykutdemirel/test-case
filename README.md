# test-case
TestCase klasörü içinde sadece 
docker-compose up --build komutunu çalıştırın

localhost'unuzun 80 port'unda çalışan bir uygulamanız olmadığından emin olunuz. Tüm Image'lar ayağa kalktıktan sonra localhost adresinden kontrol edebilirsiniz otomatik olarak swagger karşınıza çıkacaktır.

news post ettiğinizde rabbitmq'ya düşer, mongo ve elasticsearch için ayrı ayrı consume edilir.

MongoDB için herhangi bir nosql management tool ile kontrol edilebilir.
Elasticsearch kontrolü için kibana ekledim oradan takip edilebilir.
Rabbitmq için ise kullanıcı guest, şifre guest'tir. (management panel mevcut)

Tüm portlar compose dosyasında olduğundan, hangi uygulama hangi portta tek tek belirtmedim.
