using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CardFlippingGame
{
    internal class GameUI
    {
        Form f;
        private PictureBox Start = new PictureBox();
        private PictureBox Restart = new PictureBox();
        private PictureBox Back = new PictureBox();
        private PictureBox De = new PictureBox();
        private PictureBox TrungBinh = new PictureBox();
        private PictureBox Kho = new PictureBox();
        private PictureBox HaiXBa = new PictureBox();
        private PictureBox BaXBon = new PictureBox();
        private PictureBox BonXBon = new PictureBox();
        private PictureBox Thua=new PictureBox();
        private GameLogic gameLogic = new GameLogic();
        public GameManage gameManage = new GameManage();
        Panel pn=new Panel();
        public int SoDongBang;
        public int SoCotBang;
        public string dokho;
        public string Theme;
        List<PictureBox> ThemeList = new List<PictureBox>();
       

        public void BackGroundImage(Form f, string m)//Quản lý Background 
        {
            this.f = f;
            if (m == "ThemePokemon")
            {
                f.BackgroundImage = Image.FromFile(@"D:\CardFlippingGame\Picture\PicturePokemon\BackGroundPokemon.png");
            }
            else
                if (m == "ThemeTarot")
            {
                f.BackgroundImage = Image.FromFile(@"D:\CardFlippingGame\Picture\PictureTarot\BackGroundTarot.png");
            }
            else
            {
                f.BackgroundImage = Image.FromFile(@"D:\CardFlippingGame\Picture\Theme\StartBackGroundImage.png");
            }
            f.BackgroundImageLayout = ImageLayout.Stretch;
        }
        public void f_resize(object sender, EventArgs e) //điều chỉnh vị trí và chiều cao, chiều rộng nếu form thay đổi hình dạng
        {
            if (f.Contains(pn) == false)
            {
                pn.Dock = DockStyle.Fill;
                pn.BackColor = Color.Black;
                f.Controls.Add(pn);
            }
            Timer cho=new Timer();
            cho.Interval = 1;
            cho.Tick += (s2, e2) =>
            {
                cho.Stop();
                cho.Dispose();
                if (Start.Visible == true)
                {
                    Start.Height = f.ClientRectangle.Height / 8;
                    Start.Width = f.ClientRectangle.Width / 9;
                    Start.Top = (f.ClientSize.Height - Start.Height) / 2;
                    Start.Left = (f.ClientSize.Width - Start.Width) / 2;
                } //điều chỉnh cho nút start
                if (Restart.Visible == true)
                {
                    Restart.Height = f.ClientRectangle.Height / 8;
                    Restart.Width = f.ClientRectangle.Width / 9;
                    Restart.Top = (f.ClientSize.Height - Start.Height) / 2;
                    Restart.Left = (f.ClientSize.Width - Start.Width) / 2;
                } //điều chỉnh cho nút restart
                if(Back.Visible== true) {
                    Back.Width = f.ClientRectangle.Width / 11;
                    Back.Height = f.ClientRectangle.Height / 12;
                }                                                          //điều chỉnh cho nút back
                if (De.Visible == true && TrungBinh.Visible == true && Kho.Visible == true) 
                {
                    List<PictureBox> NutDoKho = new List<PictureBox>() {
                De,
                TrungBinh,
                Kho,
                };
                    int height = Start.Height / 2;
                    int width = Start.Width;
                    De.Tag = TrungBinh.Tag = Kho.Tag = "DoKho";
                    De.Name = "De"; TrungBinh.Name = "TrungBinh"; Kho.Name = "Kho";
                    De.Height = TrungBinh.Height = Kho.Height = height;
                    De.Width = TrungBinh.Width = Kho.Width = width;
                    int x = Start.Top + (Start.Height + 10);
                    int y = Start.Left - (width + 10);
                    int i = 0;
                    foreach (PictureBox pb in NutDoKho)
                    {
                        pb.Top = x;
                        pb.Left = y + i * (width + 10);
                        i++;
                    }

                }      //điều chỉnh cho các nút độ khó
                if (HaiXBa.Visible == true && BaXBon.Visible == true && BonXBon.Visible == true)
                {
                    List<PictureBox> NutKieuBang = new List<PictureBox>() {
                    HaiXBa,
                    BaXBon,
                    BonXBon,
                };
                    int height = De.Height;
                    int width = De.Width;
                    int x = De.Top + height + 10;
                    int y = De.Left;
                    int i = 0;
                    foreach (PictureBox p in NutKieuBang)
                    {
                        p.Height = height; 
                        p.Width = width;
                        p.Top = x;
                        p.Left = y + i * (De.Width + 10);
                        i++;
                    }
                } //điều chỉnh cho các nút chọn kiểu bảng
                if (gameLogic.CardList.Count>0)
                {
                    gameLogic.f_resize();
                }                                                  //điều chỉnh cho các thẻ
                if (ThemeList.Count > 0)
                {
                    int TSBheight = f.ClientRectangle.Height / 2;
                    int TSBwidth = f.ClientRectangle.Width / 6;
                    int SoTheme = 2;
                    int KhoangCach = 10;
                    int ChieuCaoBangChonTheme = TSBheight;
                    int ChieuRongBangChonTheme = (TSBwidth * SoTheme + (SoTheme - 1) * KhoangCach);
                    int y = (f.ClientRectangle.Width - ChieuRongBangChonTheme) / 2;
                    int x = (f.ClientRectangle.Height - ChieuCaoBangChonTheme) / 2;
                    int i = 0;
                    foreach (PictureBox pb in ThemeList)
                    {
                        if(pb.Visible== true)
                        {
                            pb.Height= TSBheight;
                            pb.Width= TSBwidth;
                            pb.Top = x;
                            pb.Left = (y + i * (TSBwidth + KhoangCach));
                            i++;
                        }
                    }
                }                                                         //điều chỉnh cho các nút chọn theme
                if(Thua.Visible== true)
                {
                    int height = f.ClientRectangle.Height / 5;
                    int width = f.ClientRectangle.Width / 3;
                    int x = (f.ClientRectangle.Height - height) / 2 - height - height * 1 / 2;
                    int y = (f.ClientRectangle.Width - width) / 2;
                    Thua.Height = height;
                    Thua.Width = width;
                    Thua.Top = x;
                    Thua.Left = y;
                    Thua.Visible = true;
                }                                                          //điều chỉnh cho thông báo thua
            }; cho.Start();
            pn.BringToFront();
            pn.Visible = true;
            Timer cho2 = new Timer();
            cho2.Interval = 600;
            cho2.Tick += (s2, e2) =>
            {
                cho2.Stop();
                pn.Visible = false;
            }; cho2.Start();

        }
        public void ThemeSelectionButton(Form f) //các nút chọn theme
        {
            int TSBheight = f.ClientRectangle.Height / 2;
            int TSBwidth = f.ClientRectangle.Width / 6;
            int SoTheme = gameManage.Theme.Count;
            int KhoangCach = 10;
            int ChieuCaoBangChonTheme = TSBheight;
            int ChieuRongBangChonTheme = (TSBwidth * SoTheme + (SoTheme - 1) * KhoangCach);
            int y = (f.ClientRectangle.Width - ChieuRongBangChonTheme) / 2;
            int x = (f.ClientRectangle.Height - ChieuCaoBangChonTheme) / 2;
            if (ThemeList.Count == 0)
            {
                int i = 0;
                foreach (string c in gameManage.Theme)
                {
                    PictureBox pb = new PictureBox()
                    {
                        Height = TSBheight,
                        Width = TSBwidth,
                        Top = x,
                        Left = (y + i * (TSBwidth + KhoangCach)),
                        Image = Image.FromFile(c),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = "Theme",
                    };
                    if (c.EndsWith("ThemePokemon.png"))
                    {
                        pb.Name = "ThemePokemon";
                    }
                    else if (c.EndsWith("ThemeTarot.png"))
                    {
                        pb.Name = "ThemeTarot";
                    }
                    i++;
                    f.Controls.Add(pb);
                    pb.Click += pb_ThemeClick;
                    ThemeList.Add(pb);
                }
            }
            else if (ThemeList.Count != 0)
            {
                int i = 0;
                foreach (PictureBox pb in ThemeList)
                {
                    pb.Top = x;
                    pb.Left = (y + i * (TSBwidth + KhoangCach));
                    pb.Width = TSBwidth;
                    pb.Height = TSBheight;
                    i++;
                    pb.Visible = true;
                }
            }
        }
        public void pb_ThemeClick(object sender, EventArgs e) //sự kiện click của các nút chọn theme
        {
            if (f.Contains(pn) == false)
            {
                pn.Dock = DockStyle.Fill;
                pn.BackColor = Color.Black;
                f.Controls.Add(pn);
            }
            Timer cho = new Timer();
            cho.Interval = 1;
            cho.Tick += (s2, e2) => {
                cho.Stop();
                cho.Dispose();
                Theme = " ";
                PictureBox pb = sender as PictureBox;
                string name = pb.Name;
                foreach (Control p in f.Controls) //ẩn các nút chọn theme
                {
                    if (p is PictureBox && p.Tag is string tag && tag == "Theme")
                        p.Visible = false;
                }
                start();
                Theme = name;
                BackGroundImage(f, Theme);
                back();
                if (De.BorderStyle != BorderStyle.Fixed3D)
                {
                    De.BorderStyle = BorderStyle.Fixed3D;
                    dokho = "De";
                    foreach (Control p in f.Controls)
                    {
                        if (p != De && (string)p.Tag == "DoKho" && p is PictureBox d)
                        {
                            d.BorderStyle = BorderStyle.None;
                        }
                    }
                } //cho độ khó dễ làm mặc định khi chọn theme
                DoKho();
                if (HaiXBa.BorderStyle != BorderStyle.Fixed3D)
                {
                    HaiXBa.BorderStyle = BorderStyle.Fixed3D;
                    SoDongBang = 2;
                    SoCotBang = 3;
                    foreach (Control p in f.Controls)
                    {
                        if (p != HaiXBa && (string)p.Tag == "KieuBang" && p is PictureBox d)
                        {
                            d.BorderStyle = BorderStyle.None;
                        }
                    }

                } // cho 2x3 làm mặc định sau khi chọn theme 
                KieuBang();
            }; cho.Start();
            pn.Visible = true;
            pn.BringToFront();
            Timer cho2 = new Timer();
            cho2.Interval = 200;
            cho2.Tick += (s2, e2) => {
                cho2.Stop();
                cho2.Dispose();
                pn.Visible = false;
            }; cho2.Start();
        }
        public void back() //nút Back
        {
            if (f.Controls.Contains(Back))
            {
                Back.Visible = true;
            }
            else
            {
                Back.Image = Image.FromFile(@"D:\CardFlippingGame\Picture\UIcontrols\Back.png");
                Back.Width = f.ClientRectangle.Width / 11;
                Back.Height = f.ClientRectangle.Height / 12;
                Back.Top = 0;
                Back.Left = 0;
                Back.SizeMode = PictureBoxSizeMode.StretchImage;
                f.Controls.Add(Back);
                Back.Click += pb_Backclick;
            }

        }
        public void pb_Backclick(object sender, EventArgs e) //sự kiện click của nút Back
        {

            Timer cho = new Timer();
            cho.Interval = 1;
            cho.Tick += (s2, e2) =>
            {
                cho.Stop();
                cho.Dispose();
                List<Control> removelist = new List<Control>();
                foreach (Control c in f.Controls)
                {
                    if (c is PictureBox && c.Tag is Card)
                    {
                        removelist.Add(c);
                    }
                }

                foreach (Control c in removelist)
                {
                    f.Controls.Remove(c);
                    c.Dispose();
                }

                f.BackgroundImage = Image.FromFile(@"D:\CardFlippingGame\Picture\Theme\StartBackGroundImage.png");
                f.BackgroundImageLayout = ImageLayout.Stretch;

                ThemeSelectionButton(f);
                Back.Visible = false;
                Start.Visible = false;
                Restart.Visible = false;
                De.Visible = false;
                TrungBinh.Visible = false;
                Kho.Visible = false;
                HaiXBa.Visible = false;
                BaXBon.Visible = false;
                BonXBon.Visible = false;
                Thua.Visible = false;
            }; cho.Start();
            pn.BringToFront();
            pn.Visible = true;
            Timer cho2 = new Timer();
            cho2.Interval = 600;
            cho2.Tick += (s2, e2) =>
            {
                cho2.Stop();
                cho2.Dispose();
                pn.Visible = false;
            }; cho2.Start();
        }
        public void start() //nút bắt đầu
        {
            
            if (f.Controls.Contains(Start))
            {
                Start.Height = f.ClientRectangle.Height / 8;
                Start.Width = f.ClientRectangle.Width / 9;
                Start.Top = (f.ClientSize.Height - Start.Height) / 2;
                Start.Left = (f.ClientSize.Width - Start.Width) / 2;
                Start.Visible= true;
            }
            else
            {
                Start.Name = "pb_start";
                Start.Image = Image.FromFile(@"D:\CardFlippingGame\Picture\UIcontrols\Start.png");
                Start.SizeMode = PictureBoxSizeMode.StretchImage;
                Start.Height = f.ClientRectangle.Height / 8;
                Start.Width = f.ClientRectangle.Width/9;
                Start.Top = (f.ClientSize.Height - Start.Height) / 2;
                Start.Left = (f.ClientSize.Width - Start.Width) / 2;
                f.Controls.Add(Start);
                Start.Click += pb_StartClick;
            }
        }
        public void pb_StartClick(object sender, EventArgs e) //sự kiện click của nút bắt đầu
        {
            Timer cho=new Timer();
            cho.Interval = 1;
            cho.Tick += (s2, e2) =>
            {
                cho.Stop();
                cho.Dispose();
                gameLogic.TaoManChoi(f, this);
                Start.Visible = false;
                De.Visible = false;
                TrungBinh.Visible = false;
                Kho.Visible = false;
                HaiXBa.Visible = false;
                BaXBon.Visible = false;
                BonXBon.Visible = false;
            }; cho.Start();
            pn.BringToFront();
            pn.Visible = true;
            Timer cho2 = new Timer();
            cho2.Interval = 600;
            cho2.Tick += (s2, e2) =>
            {
                cho2.Stop();
                cho2.Dispose();
                pn.Visible = false;
            }; cho2.Start();
        }
        public void DoKho()
        {
            int height = Start.Height / 2;
            int width = Start.Width;
            int x = Start.Top + (Start.Height + 10);
            int y = Start.Left - (width + 10);
            List<PictureBox> NutDoKho = new List<PictureBox>() {
                De,
                TrungBinh,
                Kho,
                };
            if (f.Controls.Contains(De) && f.Controls.Contains(TrungBinh) && f.Controls.Contains(Kho))
            {
                int i = 0;
                foreach (PictureBox pb in NutDoKho)
                {
                    pb.Top = x;
                    pb.Left = y + i * (width + 10);
                    De.Height = TrungBinh.Height = Kho.Height = height;
                    De.Width = TrungBinh.Width = Kho.Width = width;
                    i++;
                }
                De.Visible = true;
                TrungBinh.Visible = true;
                Kho.Visible = true;
            }
            else
            {
                foreach (string s in gameManage.DoKho)
                {
                    if (s.EndsWith("De.png"))
                    {
                        De.Image = Image.FromFile(s); De.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (s.EndsWith("TrungBinh.png"))
                    {
                        TrungBinh.Image = Image.FromFile(s); TrungBinh.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (s.EndsWith("Kho.png"))
                    {
                        Kho.Image = Image.FromFile(s); Kho.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                De.Tag = TrungBinh.Tag = Kho.Tag = "DoKho";
                De.Name = "De"; TrungBinh.Name = "TrungBinh"; Kho.Name = "Kho";
                De.Height = TrungBinh.Height = Kho.Height = height;
                De.Width = TrungBinh.Width = Kho.Width = width;
                int i = 0;
                foreach (PictureBox pb in NutDoKho)
                {
                    pb.Top = x;
                    pb.Left = y + i * (width + 10);
                    i++;
                    f.Controls.Add(pb);
                    pb.Click += pb_DoKhoClick;
                }

            }
        } //các nút độ khó
        public void pb_DoKhoClick(object sender, EventArgs e) //sự kiện click của các nút chọn đô khó
        {
            List<PictureBox> DoKho = new List<PictureBox>() {
                De,
                TrungBinh,
                Kho,
            };
            PictureBox pb = sender as PictureBox;
            Timer Cho = new Timer();
            Cho.Interval = 1;
            Cho.Tick += (s, e2) =>
            {
                Cho.Stop();
                Cho.Dispose();
                if (pb.BorderStyle == BorderStyle.Fixed3D)
                {
                    pb.BorderStyle = BorderStyle.None;
                }
                else
                {
                    pb.BorderStyle = BorderStyle.Fixed3D;
                    dokho = pb.Name;

                }
            };
            Cho.Start();
            Cho.Interval = 2;
            Cho.Tick += (s, e2) =>
            {
                Cho.Stop();
                Cho.Dispose();
                foreach (PictureBox p in DoKho)
                {
                    if (p != pb)
                        p.BorderStyle = BorderStyle.None;
                }
            };
            Cho.Start();
        }
        public void KieuBang() //các nút chọn kiểu bảng vd: 2x3,3x4...
        {
            List<PictureBox> NutKieuBang = new List<PictureBox>() {
                    HaiXBa,
                    BaXBon,
                    BonXBon,
                };
            int height = De.Height;
            int width = De.Width;
            int x = De.Top + height + 10;
            int y = De.Left;
            if (f.Contains(HaiXBa) && f.Contains(BaXBon) && f.Contains(BonXBon))
            {
                int i = 0;
                foreach (PictureBox p in NutKieuBang)
                {
                    p.Top = x;
                    p.Left = y + i * (De.Width + 10);
                    p.Height = height;
                    p.Width = width;
                    i++;
                }
                HaiXBa.Visible = true;
                BaXBon.Visible = true;
                BonXBon.Visible = true;
            }
            else
            {

                foreach (string pb in gameManage.KieuBang)
                {
                    if (pb.EndsWith("2x3.png"))
                    {
                        HaiXBa.Image = Image.FromFile(pb); HaiXBa.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                        if (pb.EndsWith("3x4.png"))
                    {
                        BaXBon.Image = Image.FromFile(pb); BaXBon.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                        if (pb.EndsWith("4x4.png"))
                    {
                        BonXBon.Image = Image.FromFile(pb); BonXBon.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                int i = 0;
                foreach (PictureBox p in NutKieuBang)
                {
                    p.Top = x;
                    p.Left = y + i * (De.Width + 10);
                    p.Height = height;
                    p.Width = width;
                    i++;
                    p.Click += pb_KieuBangClick;
                    f.Controls.Add(p);
                }
                HaiXBa.BorderStyle = BorderStyle.Fixed3D;
                HaiXBa.Tag = BaXBon.Tag = BonXBon.Tag = "KieuBang";
                HaiXBa.Name = "2x3";
                BaXBon.Name = "3x4";
                BonXBon.Name = "4x4";
            }
        }
        public void pb_KieuBangClick(object sender, EventArgs e) //sự kiện click của các nút chọn kiểu bảng
        {
            PictureBox pb = sender as PictureBox;
            Timer Cho = new Timer();
            List<PictureBox> list = new List<PictureBox>() {
                HaiXBa,
                BaXBon,
                BonXBon,
            };
            Cho.Interval = 1;
            Cho.Tick += (s, e2) =>
            {
                Cho.Stop();
                Cho.Dispose();
                if (pb.BorderStyle == BorderStyle.Fixed3D)
                    pb.BorderStyle = BorderStyle.None;
                else
                {
                    pb.BorderStyle = BorderStyle.Fixed3D;
                    SoDongBang = sodongbang(pb);
                    SoCotBang = socotbang(pb);
                }
            };
            Cho.Start();
            Cho.Interval = 2;
            Cho.Tick += (s, e2) =>
            {
                Cho.Stop();
                Cho.Dispose();
                foreach (PictureBox c in list)
                {
                    if (c != pb)
                    {
                        c.BorderStyle = BorderStyle.None;
                    }
                }
            };
            Cho.Start();
        } 
        public int sodongbang(PictureBox pb) //lấy số dòng bảng từ tên của nút kiểu bảng được click
        {
            int sodongbang = int.Parse(pb.Name.First().ToString());
            return sodongbang;
        } 
        public int socotbang(PictureBox pb) //lấy số cột bảng từ tên của nút kiểu bảng được click
        {
            int socotbang = int.Parse(pb.Name.Last().ToString());
            return socotbang;
        }
        public bool HetThe()// kiểm tra xem đã lật hết thẻ chưa
        {
            foreach (Control c in f.Controls)
            {
                if (c is PictureBox && c.Visible != false && c.Tag is Card)
                    return false;
            }
            return true;
        }
        public void restart() //nút bắt đầu lại màn chơi mới
        {
            if (f.Controls.Contains(Restart))
            {

                Restart.Height = f.ClientRectangle.Height / 8;
                Restart.Width = f.ClientRectangle.Width / 9;
                Restart.Top = (f.ClientSize.Height - Start.Height) / 2;
                Restart.Left = (f.ClientSize.Width - Start.Width) / 2;
                Restart.Visible = true;
            }
            else
            {
                Restart.Name = "pb_restart";
                Restart.Image = Image.FromFile(@"D:\on tap\CardFlippingGame\Picture\UIcontrols\Restart.png");
                Restart.SizeMode = PictureBoxSizeMode.StretchImage;
                Restart.Height = f.ClientRectangle.Height / 8;
                Restart.Width = f.ClientRectangle.Width/ 9;
                Restart.Top = (f.ClientSize.Height - Start.Height) / 2;
                Restart.Left = (f.ClientSize.Width - Start.Width) / 2;
                f.Controls.Add(Restart);
                Restart.Click += pb_RestartClick;
            }
        }
        public void pb_RestartClick(object sender, EventArgs e) //sự kiện click của nút restart
        {
            List<Control> removelist = new List<Control>();
            foreach (Control c in f.Controls)
            {
                if (c is PictureBox && c.Tag is Card) //nhập các card cũ vào danh sách remove
                {
                    removelist.Add(c);
                }
            }
            foreach (Control c in removelist) //remove các card cũ
            {
                f.Controls.Remove(c);
                c.Dispose();
            }
            //gameLogic = new GameLogic();
            gameLogic.TaoManChoi(f, this);
            Restart.Visible = false;
            Thua.Visible = false;
            gameLogic.DangCho = false;
        } 
        public void thua() //sự kiện thua
        {
            if ((gameLogic.SoLanClick - (SoDongBang*SoCotBang))/2<gameLogic.SoLanSai)
            {
                gameLogic.DangCho = true;
                int height = f.ClientRectangle.Height / 5;
                int width = f.ClientRectangle.Width / 3;
                int x = (f.ClientRectangle.Height-height)/2- height-height*1/2;
                int y = (f.ClientRectangle.Width-width)/2;
                if (f.Contains(Thua))
                {
                    Thua.Height = height;
                    Thua.Width = width;
                    Thua.Top = x;
                    Thua.Left = y;
                    Thua.Visible = true;
                }
                else { 
                    Thua.Image = Image.FromFile(@"D:\CardFlippingGame\Picture\PictureSo\Thua.png");
                    Thua.SizeMode= PictureBoxSizeMode.StretchImage;
                    Thua.Height = height;
                    Thua.Width = width;
                    Thua.Top = x;
                    Thua.Left = y;
                    f.Controls.Add(Thua);
                }
                    foreach (Control pb in f.Controls)
                    {
                        if (pb.Tag is Card&&pb is PictureBox)
                        {
                            pb.Visible = false;
                        }
                    }
                restart();
            }
        }
    }
}