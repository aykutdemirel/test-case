# test-case
TestCase klasörü içinde sadece <b>docker-compose up --build</b> komutunu çalıştırın.

localhost'unuzun 80 port'unda çalışan bir uygulamanız olmadığından emin olunuz. 

Tüm Image'lar ayağa kalktıktan sonra localhost adresinden karşınıza ui çıkacaktır.

Backend servisi 5000 portundan hizmet vermektedir.

News post ettiğinizde rabbitmq'ya düşer, mongo ve elasticsearch için ayrı ayrı consume edilir.

MongoDB için herhangi bir nosql management tool ile kontrol edilebilir.

Elasticsearch kontrolü için kibana ekledim oradan takip edilebilir.

Rabbitmq için ise kullanıcı guest, şifre guest'tir (management panel mevcut).

Tüm portlar compose dosyasında olduğundan, hangi uygulama hangi portta tek tek belirtmedim.
