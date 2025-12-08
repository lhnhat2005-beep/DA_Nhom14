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
        private PictureBox Thang=new PictureBox();
        private PictureBox BangSoCoHoi = new PictureBox();
        private GameLogic gameLogic;
        public GameManage gameManage;
        Panel pn=new Panel();
        public int SoDongBang;
        public int SoCotBang;
        public string dokho;
        public string Theme;
        List<PictureBox> ThemeList = new List<PictureBox>();
       
        public GameUI(GameManage gameManage,GameLogic gameLogic)
        {
            this.gameManage = gameManage;
            this.gameLogic = gameLogic;
        }
        public void BackGroundImage(Form f, string theme)//Quản lý Background 
        {
            this.f = f;
            f.BackgroundImageLayout = ImageLayout.Stretch;
            foreach (string c in gameManage.BackGround) {
                if (c.EndsWith(theme))
                {
                    f.BackgroundImage = Image.FromFile(c);
                    return;
                } 
            }
            f.BackgroundImage = Image.FromFile(gameManage.BackGround[0]);
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
                if (Start.Visible == true && f.Contains(Start))
                {
                    start();
                } //điều chỉnh cho nút start
                if (Restart.Visible == true&&f.Contains(Restart))
                {
                    restart();
                } //điều chỉnh cho nút restart
                if(Back.Visible== true&&f.Contains(Back)) {
                    back();
                } //điều chỉnh cho nút back
                if (De.Visible == true &&f.Contains(De)) 
                {
                    DoKho();
                } //điều chỉnh cho các nút độ khó
                if (HaiXBa.Visible == true && f.Contains(HaiXBa))
                {
                   KieuBang();
                } //điều chỉnh cho các nút chọn kiểu bảng
                if (gameLogic.CardList.Count>0)
                {
                    gameLogic.f_resize();
                } //điều chỉnh cho các thẻ
                if (ThemeList.Count > 0)
                {
                    if (ThemeList[0].Visible == true)
                    {
                        ThemeSelectionButton(f);
                    }
                } //điều chỉnh cho các nút chọn theme
                if(Thua.Visible== true&&f.Contains(Thua))
                {
                    thua();
                } //điều chỉnh cho thông báo thua
                if(Thang.Visible== true && f.Contains(Thang))
                {
                    thang();
                } //điều chỉnh cho thông báo thắng
                if (BangSoCoHoi.Visible == true && f.Contains(BangSoCoHoi)) {
                    bangsocohoi();
                } ; //điều chỉnh bảng hiển thị số cơ hội
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
                        pb.Name = "Pokemon.png";
                    }
                    else if (c.EndsWith("ThemeTarot.png"))
                    {
                        pb.Name = "Tarot.png";
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
                foreach (PictureBox p in ThemeList) //ẩn các nút chọn theme
                {
                        p.Visible = false;
                }
                start();
                Theme = pb.Name;
                BackGroundImage(f, Theme);
                back();
                if (De.BorderStyle != BorderStyle.Fixed3D)
                {
                    De.BorderStyle = BorderStyle.Fixed3D;
                    dokho = "De.png";
                    TrungBinh.BorderStyle= BorderStyle.None;
                    Kho.BorderStyle = BorderStyle.None;
                } //cho độ khó dễ làm mặc định khi chọn theme
                DoKho();
                if (HaiXBa.BorderStyle != BorderStyle.Fixed3D)
                {
                    HaiXBa.BorderStyle = BorderStyle.Fixed3D;
                    SoDongBang = 2;
                    SoCotBang = 3;
                    BaXBon.BorderStyle = BorderStyle.None;
                    BonXBon.BorderStyle = BorderStyle.None;
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
            Back.Width = f.ClientRectangle.Width / 11;
            Back.Height = f.ClientRectangle.Height / 12;
            Back.Top = 0;
            Back.Left = 0;
            if (f.Controls.Contains(Back)==false)
            {
                Back.Name = "Back.png";
                foreach (string s in gameManage.SRB)
                {
                    if (s.EndsWith(Back.Name))
                    {
                        Back.Image = Image.FromFile(s);
                    }
                }
                Back.SizeMode = PictureBoxSizeMode.StretchImage;
                f.Controls.Add(Back);
                Back.Click += pb_Backclick;
                return;
            }
            Back.Visible = true;
        }
        public void pb_Backclick(object sender, EventArgs e) //sự kiện click của nút Back
        {

            Timer cho = new Timer();
            cho.Interval = 1;
            cho.Tick += (s2, e2) =>
            {
                cho.Stop();
                cho.Dispose();
                if (Start.Visible == false)
                {
                    foreach (PictureBox c in gameLogic.CardList)
                    {
                        f.Controls.Remove(c);
                        c.Dispose();
                    }
                    gameLogic.CardList.Clear(); //làm rỗng danh sách chứa picturebox card tránh tích trữ rác
                    if (f.Contains(Restart)&&Restart.Visible==true)
                    {
                        Restart.Visible = false;
                    }
                    if (f.Contains(Thua) && Thua.Visible == true)
                    {
                        Thua.Visible = false;
                    }
                    if (f.Contains(Thang) && Thang.Visible == true)
                    {
                        Thang.Visible = false;
                    }
                    if (f.Contains(BangSoCoHoi))
                    {
                        BangSoCoHoi.Visible = false;
                    }
                    start();
                    DoKho();
                    KieuBang();
                }
                else
                {
                    f.BackgroundImage = Image.FromFile(gameManage.BackGround[0]);
                    ThemeSelectionButton(f);
                    Back.Visible = false;
                    Start.Visible = false;
                    De.Visible = false;
                    TrungBinh.Visible = false;
                    Kho.Visible = false;
                    HaiXBa.Visible = false;
                    BaXBon.Visible = false;
                    BonXBon.Visible = false;
                    
                }
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
            Start.Height = f.ClientRectangle.Height / 8;
            Start.Width = f.ClientRectangle.Width / 9;
            Start.Top = (f.ClientSize.Height - Start.Height) / 2;
            Start.Left = (f.ClientSize.Width - Start.Width) / 2;
            if (f.Controls.Contains(Start)==false)
            {
                Start.Name = "Start.png";
                foreach (string s in gameManage.SRB)
                {
                    if (s.EndsWith(Start.Name))
                    {
                        Start.Image = Image.FromFile(s);
                        f.Controls.Add(Start);
                        Start.Click += pb_StartClick;
                    }
                }
                Start.SizeMode = PictureBoxSizeMode.StretchImage;
                return;
            }
            Start.Visible = true;
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
            int x = Start.Top + (Start.Height+ 10);
            int y = Start.Left - (width + 10);
            List<PictureBox> NutDoKho = new List<PictureBox>() {
                De,
                TrungBinh,
                Kho,
                };
            int i = 0;
            foreach (PictureBox pb in NutDoKho)
            {
                pb.Top = x;
                pb.Left = y + i * (width + 10);
                pb.Height = height;
                pb.Width = width;
                i++;
            }
            if (f.Contains(De)==false)
            {
                De.Name = "De.png"; TrungBinh.Name = "TrungBinh.png"; Kho.Name = "Kho.png";
                foreach (string s in gameManage.DoKho)
                {
                    foreach (PictureBox s2 in NutDoKho) {
                        if (s.EndsWith(s2.Name))
                        {
                            s2.Image = Image.FromFile(s); s2.SizeMode = PictureBoxSizeMode.StretchImage;
                        } 
                    }
                }
                foreach (PictureBox pb in NutDoKho)
                {
                    f.Controls.Add(pb);
                    pb.Click += pb_DoKhoClick;
                    pb.Tag = "DoKho";
                }
                return;
            }
            foreach (PictureBox pb in NutDoKho)
            {
                pb.Visible = true;
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
            int i = 0;
            foreach (PictureBox p in NutKieuBang)
            {
                p.Top = x;
                p.Left = y + i * (De.Width + 10);
                p.Height = height;
                p.Width = width;
                i++;
            }
            if (f.Contains(HaiXBa)==false)
            {
                HaiXBa.Name = "2x3"; BaXBon.Name = "3x4"; BonXBon.Name = "4x4";
                foreach (string s in gameManage.KieuBang)
                {
                    foreach (PictureBox pb in NutKieuBang)
                    {
                        if (s.EndsWith(pb.Name+".png"))
                        {
                            pb.Image = Image.FromFile(s); 
                            pb.SizeMode = PictureBoxSizeMode.StretchImage;
                            pb.Click += pb_KieuBangClick;
                            f.Controls.Add(pb);
                            pb.Tag = "KieuBang";
                            break;
                        }
                    }
                }
                HaiXBa.BorderStyle = BorderStyle.Fixed3D;
                return;
            }
            foreach (PictureBox pb in NutKieuBang)
            {
                pb.Visible = true;
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
                    SoDongBang = int.Parse(pb.Name.First().ToString());
                    SoCotBang = int.Parse(pb.Name.Last().ToString());
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
        public bool HetThe()// kiểm tra xem đã lật hết thẻ chưa
        {
            foreach (PictureBox c in gameLogic.CardList)
            {
                if (c.Visible != false)
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
                Restart.Name = "Restart.png";
                foreach (string s in gameManage.SRB)
                {
                    if (s.EndsWith(Restart.Name))
                    {
                        Restart.Image = Image.FromFile(s);
                    }
                }
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
            foreach (PictureBox c in gameLogic.CardList) //remove các card cũ
            {
                f.Controls.Remove(c);
                c.Dispose();
            }
            gameLogic.CardList.Clear(); //làm rỗng danh sách chứa picturebox card tránh tích trữ rác
            gameLogic.TaoManChoi(f, this);
            Restart.Visible = false;
            if (f.Contains(Thua) && Thua.Visible == true)
            {
                Thua.Visible = false;
            }
            if (f.Contains(Thang) && Thang.Visible == true)
            {
                Thang.Visible = false;
            }
            gameLogic.DangCho = false;
        } 
        public void thua() //sự kiện thua
        {
            gameLogic.SoCoHoi--;
            if ((gameLogic.SoCoHoi==0))
            {
                gameLogic.DangCho = true;
                int height = f.ClientRectangle.Height / 5;
                int width = f.ClientRectangle.Width / 3;
                int x = (f.ClientRectangle.Height-height)/2- height-height*1/2;
                int y = (f.ClientRectangle.Width-width)/2;
                Thua.Height = height;
                Thua.Width = width;
                Thua.Top = x;
                Thua.Left = y;
                if (f.Contains(Thua)==false)
                {
                    Thua.Name = "Thua.png";
                    foreach (string s in gameManage.ThangThua)
                    {
                        if (s.EndsWith(Thua.Name))
                        {
                            Thua.Image = Image.FromFile(s);
                            Thua.SizeMode=PictureBoxSizeMode.StretchImage;
                        }
                    }
                    f.Controls.Add(Thua);
                }
                else
                    Thua.Visible = true;
                foreach (PictureBox c in gameLogic.CardList)
                {
                    c.Visible = false;
                }
               
                restart();
            }
        }
        public void thang() //sự kiện thang
        {
            if (HetThe()==true)
            {
                int height = f.ClientRectangle.Height / 5;
                int width = f.ClientRectangle.Width / 3;
                int x = (f.ClientRectangle.Height - height) / 2 - height - height * 1 / 2;
                int y = (f.ClientRectangle.Width - width) / 2;
                Thang.Height = height;
                Thang.Width = width;
                Thang.Top = x;
                Thang.Left = y;
                
                if (f.Contains(Thang)==false)
                {

                    Thang.Name = "Thang.png";
                    foreach (string s in gameManage.ThangThua)
                    {
                        if (s.EndsWith(Thang.Name))
                        {
                            Thang.Image = Image.FromFile(s);
                            Thang.SizeMode = PictureBoxSizeMode.StretchImage;
                            break;
                        }
                    }
                    f.Controls.Add(Thang);
                }else
                    Thang.Visible = true;
                restart();
            }
        }
        public void bangsocohoi()
        {
            int height= f.ClientRectangle.Height / 10;
            int width=f.ClientRectangle.Width / 11;
            BangSoCoHoi.Height = height;
            BangSoCoHoi.Width = width;
            BangSoCoHoi.Top = 0;
            BangSoCoHoi.Left = (f.ClientRectangle.Width-width);
            foreach (string s in gameManage.So)
            {
                if (s.EndsWith(gameLogic.SoCoHoi.ToString() + ".png"))
                {
                    BangSoCoHoi.Image = Image.FromFile(s);
                }
            }
            if (f.Contains(BangSoCoHoi) == false)
            {
                BangSoCoHoi.SizeMode = PictureBoxSizeMode.StretchImage;
                f.Controls.Add(BangSoCoHoi);
                return;
            }
           BangSoCoHoi.Visible = true;
        } //Bảng hiển thị số cơ hội
      
    }
}