using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CardFlippingGame
{
    internal class GameLogic
    {
        GameUI ui;
        GameManage gameManage;
        Form f;
        public List<PictureBox> CardList = new List<PictureBox>(); //list chứa các picturebox thẻ để sử dụng cho hàm f_resize
        private Card FistCard = null;
        private Card SecondCard = null;
        private PictureBox pb1 = null;
        private PictureBox pb2 = null;
        public int SoLanClick;
        public int SoCoHoi;
        public bool DangCho = false;// điều kiện ngăn không cho người chơi click liên tục 
        public void TaoManChoi(Form f, GameUI UI) //tạo màn chơi
        {
            gameManage = new GameManage();
            this.f = f;
            this.ui = UI;
            int cardheight = f.ClientRectangle.Height / 5;
            int cardwidth = f.ClientRectangle.Width / 8;
            int Khoangcach = 10;
            int sodong = ui.SoDongBang;
            int socot = ui.SoCotBang;
            int broadheight;
            int broadwidth;
            broadheight = sodong * cardheight + (sodong - 1) * Khoangcach;//tính chiều cao của bảng trò chơi 
            broadwidth = socot * cardwidth + (socot - 1) * Khoangcach;//tính chiều rộng của bảng trò chơi
            int y = (f.ClientSize.Width - broadwidth) / 2;// x và y là tọa độ của vị trí bắt đầu tạo bảng trò chơi
            int x = (f.ClientSize.Height - broadheight) / 2;
            gameManage.KhoiTaoBang(ui.Theme, ui);
            solanclick(ui);
            socohoi(SoLanClick, ui);
            ui.bangsocohoi();
            for (int i = 0; i < sodong; i++)
            {
                for (int j = 0; j < socot; j++)
                {
                    Card c = gameManage.The[i, j];
                    PictureBox pb = new PictureBox()
                    {
                        Width = cardwidth,
                        Height = cardheight,
                        Top = (x + i * (cardheight + Khoangcach)),
                        Left = (y + j * (cardwidth + Khoangcach)),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = Image.FromFile(c.BackPicture),
                        Tag = c,

                    };
                    f.Controls.Add(pb);
                    CardList.Add(pb);
                    pb.Click += pb_click;
                }
            }
        }
        public void pb_click(object sender, EventArgs e)// sự kiện click của thẻ
        {
            if (DangCho)
                return;
            PictureBox pb = sender as PictureBox;
            Card c = pb.Tag as Card;
            if (c.IsFlipped == true || c.IsMatched == true)
                return;
            pb.Image = Image.FromFile(c.Picture);
            c.FlipCard();
            if (FistCard == null)
            {
                FistCard = c;
                pb1 = pb;
                return;
            }
            else
            if (SecondCard == null)
            {
                SecondCard = c;
                pb2 = pb;
            }
            DangCho = true;
            if (FistCard.Picture == SecondCard.Picture)
            {
                Timer cho = new Timer();
                cho.Interval = 300;
                cho.Tick += (s, e2) =>
                {
                    cho.Stop();
                    cho.Dispose();
                    FistCard.IsMatched = true;
                    SecondCard.IsMatched = true;
                    pb1.Visible = false;
                    pb2.Visible = false;
                    FistCard = null;
                    SecondCard = null;
                    pb1 = null;
                    pb2 = null;
                    DangCho = false;
                    ui.thang();
                };
                cho.Start();
            }
            else
            {
                Timer ThoiGianCho = new Timer();
                ThoiGianCho.Interval = 1000;
                ThoiGianCho.Tick += (s, e2) =>
                {
                    ThoiGianCho.Stop();
                    ThoiGianCho.Dispose();
                    if (pb1 != null && pb2 != null && FistCard != null && SecondCard != null)//đề phòng null khiến logic bị lỗi
                    {
                        pb1.Image = Image.FromFile(FistCard.BackPicture);
                        pb2.Image = Image.FromFile(SecondCard.BackPicture);
                        FistCard.FlipCard();
                        SecondCard.FlipCard();
                    }

                    FistCard = null;
                    SecondCard = null;
                    DangCho = false;
                    ui.thua();
                    ui.bangsocohoi();
                };
                ThoiGianCho.Start();
            }
        }
        public void f_resize() //điều chỉnh vị trí thẻ khi màn hình phóng to thu nhỏ
        {
            int cardheight = f.ClientRectangle.Height / 5;
            int cardwidth = f.ClientRectangle.Height / 5;
            int Khoangcach = 10;
            int socot = ui.SoCotBang;
            int sodong = ui.SoDongBang;
            int broadheight;
            int broadwidth;

            broadheight = sodong * cardheight + (sodong - 1) * Khoangcach;//tính chiều cao của bảng trò chơi 
            broadwidth = socot * cardwidth + (socot - 1) * Khoangcach;//tính chiều rộng của bảng trò chơi
            int y = (f.ClientSize.Width - broadwidth) / 2;// x và y là tọa độ của vị trí bắt đầu tạo bảng trò chơi
            int x = (f.ClientSize.Height - broadheight) / 2;
            int k = 0;
            for (int i = 0; i < sodong; i++)
            {
                for (int j = 0; j < socot; j++)
                {
                    {
                        CardList[k].Left = (y + j * (cardwidth + Khoangcach));
                        CardList[k].Top = (x + i * (cardheight + Khoangcach));
                        CardList[k].Height = cardheight;
                        CardList[k].Width = cardwidth;
                        k++;
                    }
                }

            }
        }
        public void solanclick(GameUI UI) //khởi tạo số lần click dựa vào độ khó trong GameUi
        {
            double kho;
            if (UI.dokho == "De.png")
            {
                SoLanClick = UI.SoCotBang * UI.SoDongBang * 2;
            }
            else if (UI.dokho == "Kho.png")
            {
                kho = UI.SoCotBang * UI.SoDongBang * 1.2;
                kho = Math.Ceiling(kho);
                if (kho % 2 != 0)
                    kho += 1;
                SoLanClick = (int)kho;
            }
            else if (ui.dokho == "TrungBinh.png")
            {
                kho = UI.SoCotBang * UI.SoDongBang * 1.2;
                kho = Math.Ceiling(kho);
                if (kho % 2 != 0)
                    SoLanClick = (UI.SoCotBang * UI.SoDongBang * 2 + (int)kho + 1) / 2;
                else
                    SoLanClick = (UI.SoCotBang * UI.SoDongBang * 2 + (int)kho) / 2;
            }
        }
        public void socohoi(int solanclick,GameUI ui) //khởi tạo số cơ hội
        {
            SoCoHoi = (solanclick- (ui.SoDongBang * ui.SoCotBang)) / 2 + 1;
        }
    }
}

