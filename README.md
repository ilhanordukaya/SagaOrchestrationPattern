                                  Saga Orchestration Pattern


                     
    Eventual consistency
  Nihai tutarlılık, asenkron iletişim yoluyla veri tutarlılığını ve kullanılabilirliğini sağlayan ve belirli bir süreçte bir hata oluştuğunda,
 tüm süreci geri almak zorunda kalmadan hatanın eninde sonunda çözülmesini sağlayan bir tekniktir. Bu tekniklerden biri olan Saga Patten'i üzerinde duracağız.

 Saga Pattern Nedir?
Saga Pattern ile oluşturulan sistemlerde gelen istek ile daha sonraki her adım, bir önceki adımın başarılı şekilde tamamlanması sonrasında tetiklenir.
Herhangi bir failover durumunda işlemi geri alma veya bir düzeltme aksiyonu almayı sağlayan pattern’dir.

![image](https://github.com/ilhanordukaya/SagaOrchestrationPattern/assets/67738617/d491396b-9a2c-42c7-8183-bbc28194853f)


Orchestration , bir merkezi denetleyicinin sagaları koordine ettiği bir yöntemdir; bu denetleyici, saga katılımcılarına hangi yerel işlemleri gerçekleştireceklerini bildirir. Saga orkestratörü,
tüm işlemleri yönetir ve katılımcılara, olaylara dayalı olarak hangi işlemi gerçekleştireceklerini bildirir.
Orkestratör, saga isteklerini yürütür, her görevin durumlarını depolar ve yorumlar, ayrıca kompanse işlemlerle başarısızlık kurtarma işlemlerini yönetir.

Avantajlar

Çok sayıda katılımcıyı veya zamanla eklenen yeni katılımcıları içeren karmaşık iş akışları için idealdir.

Süreçteki her katılımcının kontrolü ve faaliyetlerin akışı üzerinde kontrol olduğunda uygundur.

Orkestratör tek taraflı olarak destan katılımcılarına bağlı olduğundan döngüsel bağımlılıklar getirmez.

Saga katılımcılarının diğer katılımcıların komutlarını bilmesine gerek yoktur. Endişelerin net bir şekilde ayrılması iş mantığını basitleştirir.

Dezavantajlar

Ek tasarım karmaşıklığı, bir koordinasyon mantığının uygulanmasını gerektirir.

Orkestratörün tüm iş akışını yönetmesi nedeniyle ek bir başarısızlık noktası daha vardır.

