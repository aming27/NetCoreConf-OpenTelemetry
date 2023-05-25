namespace Marvel.Api.Data;

public class Seed
{
    public static readonly List<Avenger> Avengers = new List<Avenger>
{
    new Avenger { Id = 1, Name = "Thor", Photo= "https://heroichollywood.com/wp-content/uploads/2019/05/Thor-Chris-Hemsworth-Avengers-Endgame.jpg"},
    new Avenger { Id = 2, Name = "Hulk", Photo= "https://i.kinja-img.com/gawker-media/image/upload/s--gA6KuMPG--/c_scale,f_auto,fl_progressive,q_80,w_800/qtlzkj1nt13ybo6e3an8.jpg" },
    new Avenger { Id = 3, Name = "Iron Man", Photo= "https://i.pinimg.com/originals/62/bd/03/62bd03c83006f62807a4feadc6efa754.jpg" },
    new Avenger { Id = 4, Name = "Capitan Ámeric", Photo="https://img-cdn.hipertextual.com/files/2018/11/avengers-infinity-war-captain-america.jpg?strip=all&lossy=1&quality=70&resize=740%2C490&ssl=1" },
    new Avenger { Id = 5, Name = "Viuda Negra", Photo="http://es.web.img3.acsta.net/newsv7/18/12/30/15/11/1409920.jpg" },
    new Avenger { Id = 6, Name = "Ojo de Halcon", Photo="https://www.hindustantimes.com/rf/image_size_960x540/HT/p2/2019/04/14/Pictures/_b928d83c-5ea1-11e9-93dc-bd285d0e4b85.jpg" },    
};

}
