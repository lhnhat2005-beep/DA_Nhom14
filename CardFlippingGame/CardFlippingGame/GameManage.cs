using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardFlippingGame
{
    internal class GameManage
    {
        public List<string> KieuBang=new List<string>() {
            @"D:\CardFlippingGame\Picture\UIcontrols\2x3.png",
            @"D:\CardFlippingGame\Picture\UIcontrols\3x4.png",
            @"D:\CardFlippingGame\Picture\UIcontrols\4x4.png",
        }; //list chứa ảnh của các nút kiểu bảng vd: 3*4,2*3,...
        public List<string> DoKho = new List<string>() {
            @"D:\CardFlippingGame\Picture\UIcontrols\De.png",
            @"D:\CardFlippingGame\Picture\UIcontrols\TrungBinh.png",
            @"D:\CardFlippingGame\Picture\UIcontrols\Kho.png"
        };//list chứa ảnh của các nút độ khó
        public List<string> Theme = new List<string> 
        {
            @"D:\CardFlippingGame\Picture\Theme\ThemePokemon.png",
            @"D:\CardFlippingGame\Picture\Theme\ThemeTarot.png",
        }; //list chứa ảnh của các nút chọn theme
        public List<string> BackGround = new List<string> {
            @"D:\CardFlippingGame\Picture\PicturePokemon\BackGroundPokemon.png",
            @"D:\CardFlippingGame\Picture\PictureTarot\BackGroundTarot.png"
        }; //list chứa ảnh background của các theme
        List<string> PictureListTarot = new List<string> 
        {
            @"D:\CardFlippingGame\Picture\PictureTarot\Tarot1.png",
            @"D:\CardFlippingGame\Picture\PictureTarot\Tarot2.png",
            @"D:\CardFlippingGame\Picture\PictureTarot\Tarot3.png",
            @"D:\CardFlippingGame\Picture\PictureTarot\Tarot4.png",
            @"D:\CardFlippingGame\Picture\PictureTarot\Tarot5.png",
            @"D:\CardFlippingGame\Picture\PictureTarot\Tarot6.png",
            @"D:\CardFlippingGame\Picture\PictureTarot\Tarot7.png",
            @"D:\CardFlippingGame\Picture\PictureTarot\Tarot8.png",
        };//list chứa ảnh Card theme Tarot
        List<string> PictureListPokemon = new List<string>
        {
           @"D:\CardFlippingGame\Picture\PicturePokemon\Pokemon1.png",
           @"D:\CardFlippingGame\Picture\PicturePokemon\Pokemon2.png",
           @"D:\CardFlippingGame\Picture\PicturePokemon\Pokemon3.png",
           @"D:\CardFlippingGame\Picture\PicturePokemon\Pokemon4.png",
           @"D:\CardFlippingGame\Picture\PicturePokemon\Pokemon5.png",
           @"D:\CardFlippingGame\Picture\PicturePokemon\Pokemon6.png",
           @"D:\CardFlippingGame\Picture\PicturePokemon\Pokemon7.png",
           @"D:\CardFlippingGame\Picture\PicturePokemon\Pokemon8.png",
        }; //list chứa ảnh Card theme Pokemon
        List<string> Listx2 = new List<string>();//List Chứa Ảnh Sau Nhân 2 Để Nhập Dô Mảng 2 Chiều
        Random rand = new Random();
        public Card[,] The;
        public void x2(string m,GameUI UI)//x2 Từng Phần Tử Trong PictureList
        {
            if (m == "Tarot.png") {
                FisherYates(PictureListTarot);
                for(int i=0;i<UI.SoCotBang*UI.SoDongBang/2;i++)
                {
                    Listx2.Add(PictureListTarot[i]);
                    Listx2.Add(PictureListTarot[i]);
                }
            }
            else if(m == "Pokemon.png") {
                FisherYates(PictureListPokemon);
                for (int i = 0; i < UI.SoCotBang * UI.SoDongBang / 2; i++)
                {
                    Listx2.Add(PictureListPokemon[i]);
                    Listx2.Add(PictureListPokemon[i]);
                }
            }
        }
        public void the(GameUI UI) //tạo mảng 2 chiều với các phần tử là Card
        {
            The = new Card[UI.SoDongBang, UI.SoCotBang];
        } 
        public void FisherYates(List<string> list) //hàm trộn các phần tử trong danh sach
        {
            for (int i = list.Count-1;i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        } 
        public void NhapMang(GameUI UI) //gán các phần tử của listx2 dô mảng 2 chiều The
        {
            int k = 0;
            for (int i = 0; i < UI.SoDongBang; i++)
            {
                for (int j = 0; j < UI.SoCotBang; j++)
                {
                    The[i, j] = new Card(Listx2[k]);
                    k++;
                }
            }
        } 
        public void KhoiTaoBang(string m,GameUI UI) //khởi tạo bảng
        {
            the(UI);
            x2(m,UI);
            FisherYates(Listx2);
            NhapMang(UI);
        } 
    }
}
