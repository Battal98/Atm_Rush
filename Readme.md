# ATM RUSH CLONE 
Level-Up Akademi süreci boyunca 3 ekip arkadaşımla birlikte yapmış olduğumuz ATM Rush oyununun kopyasının kaynak kodlarını sizlerle paylaşıyoruz.

## Proje Süreci ve Yönetimi
Projeye başlamadan önce ekip arkadaşlarımla birlikte UML Diagram oluşturarak ilerleyeceğimiz yolda bir harita çizildi. 
Genel bir çok durumu oyunun orijinalini inceleyerek düşünmeye çalıştık ve aşağıda görmüş olduğunuz UML diagramı oluşturuldu.

![image](https://user-images.githubusercontent.com/68375602/182611639-d04ae482-c368-4480-9f24-42f47ae495c0.png)

Diagramı oluştururken bazı eksik durumları oyunun yapım esnasında tekrar düzenleyerek oyuna entegre işlemi sorunsuz bir şekilde gerçekleştirildi. 
Genel kod mimarisi üzerine öğrendiğimiz gelişmeleri bu oyunda kullanarak diğer kodlara olan bağımlılığı en aza indirgeyecek senaryoları oluşturarak 
oyunumuzda temel olarak Observer Pattern kullanıldı. Bununla birlikte Command Pattern, Scriptable Object, Facade Pattern, Singleton gibi yapılardan da yararlanarak
daha anlaşılır ve düzgün bir kod yapısı oluşturuldu.

## Kullanılan Ek Paketler

* Toony Colors 
* DOTween
* Easy Save
* GUI Packages
* Epic Toon

## Yapılan Mekanikler
* Object Lerp
* Player Movement
* Object Scale size up and down
* Minigame
* Atm deposit Mechanic
* Change Mesh
* Obstacle Animation and Collision

![AtmRush_mpeg4 (1)](https://user-images.githubusercontent.com/68375602/182850906-56cad8ec-74b5-4b0a-a41c-71717b804427.gif)

