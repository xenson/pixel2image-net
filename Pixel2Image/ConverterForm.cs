using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel2Image
{
    public partial class ConverterForm : Form
    {
        //行:列=69:49
        private const int NEW_IMG_HEIGHT = 69;
        private const string NEW_IMG_NAME = "DiceImage.jpg";
        private const string TEXT_FILE_NAME = "DiceArrangement.txt";

        private Bitmap[] dice = new Bitmap[6];

        public ConverterForm()
        {
            InitializeComponent();
            LoadDiceImages();
        }

        //加载元素图片列表
        private void LoadDiceImages()
        {
            for (int i = 0; i < 6; i++)
            {
                string imagePath = Path.Combine(Application.StartupPath, "Images", $"dice{i + 1}.png");
                dice[i] = new Bitmap(imagePath).Resize(64, 64);
            }
        }


        private void ConvertToDiceImage(string filename)
        {
            Bitmap img = new Bitmap(filename);
            Bitmap grayImage = img.ConvertToGrayscale();
            double relativeSize = (double)img.Width / img.Height;
            Bitmap smallImg = grayImage.Resize((int)(NEW_IMG_HEIGHT * relativeSize), NEW_IMG_HEIGHT);

            int[,] pixelMatrix = smallImg.ToMatrix();
            int newWidth = (int)(NEW_IMG_HEIGHT * relativeSize * 64) - 64;
            Bitmap diceImg = new Bitmap(newWidth, NEW_IMG_HEIGHT * 64);

            using (StreamWriter writer = new StreamWriter(TEXT_FILE_NAME))
            using (Graphics g = Graphics.FromImage(diceImg))
            {
                for (int row = 0; row < NEW_IMG_HEIGHT; row++)
                {
                    int lastDice = 0;
                    int diceCounter = 0;
                    string line = "";
                    for (int column = 0; column < (int)(NEW_IMG_HEIGHT * relativeSize); column++)
                    {
                        int greyValue = pixelMatrix[row, column];
                        int diceNumber = AssignDiceToColor(greyValue);
                        diceImg.Paste(dice[diceNumber], column * 64, row * 64);

                        int currentDice = diceNumber + 1;
                        if (currentDice != lastDice && diceCounter != 0)
                        {
                            line += $"d{lastDice} x {diceCounter}, ";
                            diceCounter = 1;
                        }
                        else
                        {
                            diceCounter++;
                        }
                        lastDice = currentDice;

                        //添加行列标注
                        g.DrawString($"R:{row + 1} C:{column + 1}", new Font("Arial", 6), Brushes.Red, column * 64, row * 64);
                    }
                    line += $"d{lastDice} x {diceCounter}";
                    writer.WriteLine(line);
                }
            }

            diceImg.Save(NEW_IMG_NAME);
            MessageBox.Show("Done!");
            double relWi = 1.6 * NEW_IMG_HEIGHT * relativeSize;
            double relHi = 1.6 * NEW_IMG_HEIGHT;

            this.reultText.Text = "In real life this image would measure: " + ((int)relHi) + "cm * " + ((int)relWi) + "cm";
        }

        //灰度转值区间均分为替换元素
        private int AssignDiceToColor(int greyValue)
        {
            return 5 - (int)(greyValue / 45);
        }

        //选择需要进行像素转换的图片
        private void choiceButton_Click(object sender, EventArgs e)
        {
            this.reultText.Text = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imageFileName = openFileDialog.FileName;
                ConvertToDiceImage(imageFileName);
            }
        }

    }

    //自定义Bitmap类扩展方法
    public static class BitmapExtensions
    {
        public static Bitmap Resize(this Bitmap image, int width, int height)
        {
            return new Bitmap(image, new Size(width, height));
        }

        //转换图片为黑白灰度
        public static Bitmap ConvertToGrayscale(this Bitmap image)
        {
            Bitmap grayscale = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color c = image.GetPixel(i, j);
                    int gray = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    grayscale.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }

            return grayscale;
        }

        //获取相对灰度值
        public static int[,] ToMatrix(this Bitmap image)
        {
            int[,] matrix = new int[image.Height, image.Width];

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color color = image.GetPixel(j, i);
                    matrix[i, j] = color.R; // assuming grayscale, so R=G=B
                }
            }

            return matrix;
        }

        //坐标像素替换
        public static void Paste(this Bitmap destBitmap, Bitmap sourceBitmap, int x, int y)
        {
            using (Graphics g = Graphics.FromImage(destBitmap))
            {
                g.DrawImage(sourceBitmap, x, y);
            }
        }
    }

}
