using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            @"2x3.png",
            @"3x4.png",
            @"4x4.png",
        }; //list chứa ảnh của các nút kiểu bảng vd: 3*4,2*3,...
        public List<string> DoKho = new List<string>() {
            @"De.png",
            @"TrungBinh.png",
            @"Kho.png"
        };//list chứa ảnh của các nút độ khó
        public List<string> Theme = new List<string> 
        {
            @"ThemePokemon.png",
            @"ThemeTarot.png",
        }; //list chứa ảnh của các nút chọn theme
        public List<string> BackGround = new List<string> {
            @"StartBackGroundImage.png",
            @"BackGroundPokemon.png",
            @"BackGroundTarot.png",
        }; //list chứa ảnh background của các theme
        public List<string> SRB = new List<string>() {
            @"Start.png",
            @"Restart.png",
            @"Back.png",
        }; //list chứa ảnh của các nút Start,Restart và Back
        public List<string> BackPicture = new List<string> {
            @"BackPicturePokemon.png",
            @"BackPictureTarot.png",
        }; //list chứa ảnh mặt sau của của các thẻ theo theme
        public List<string> ThangThua = new List<string>()
        {
            @"Thua.png",
            @"Thang.png",
        };
        List<string> PictureListTarot = new List<string> 
        {
            @"Tarot1.png",
            @"Tarot2.png",
            @"Tarot3.png",
            @"Tarot4.png",
            @"Tarot5.png",
            @"Tarot6.png",
            @"Tarot7.png",
            @"Tarot8.png",
        };//list chứa ảnh Card theme Tarot
        List<string> PictureListPokemon = new List<string>
        {
           @"Pokemon1.png",
           @"Pokemon2.png",
           @"Pokemon3.png",
           @"Pokemon4.png",
           @"Pokemon5.png",
           @"Pokemon6.png",
           @"Pokemon7.png",
           @"Pokemon8.png",
        }; //list chứa ảnh Card theme Pokemon
        List<string> Listx2 = new List<string>();//List Chứa Ảnh Sau Nhân 2 Để Nhập Dô Mảng 2 Chiều
        Random rand = new Random();
        public Card[,] The;

        public void Gan(List<string> l,string m,string n) //Gán đường tới phần tử của list được chọn
        {
            for (int i = 0; i < l.Count; i++)
            {
                l[i]=Path.Combine(Application.StartupPath,m,n, l[i]);
            }
        }
        public void Chinh() //sử dụng Gan cho các list dùng trong GameUI
        {
            string p = "Picture";
            string uic = "UIcontrols";
            string t = "Theme";
            string ps = "PictureSo";
            Gan(KieuBang, p,uic);
            Gan(DoKho, p, uic);
            Gan(Theme,p,t);
            Gan(BackGround, p, t);
            Gan(SRB, p, uic);
            Gan(ThangThua, p, ps);
        }

        public void ChinhThe() //sử dụng Gan cho các list dùng trong GameLogic
        {
            string p = "Picture";
            string pt = "PictureTarot";
            string pp = "PicturePokemon";
            string t = "Theme";
            Gan(PictureListTarot, p, pt);
            Gan(PictureListPokemon, p, pp);
            Gan(BackPicture, p, t);
        }
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
            ChinhThe();
            the(UI);
            x2(m,UI);
            FisherYates(Listx2);
            NhapMang(UI);
        } 
    }
}
