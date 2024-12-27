namespace RandomPictureSelector
{
    public partial class RandomForm : Form
    {
        private List<string> imagePaths = new List<string>(); // Paths for listBox1
        private List<string> usedImagePaths = new List<string>(); // Paths for listBox2
        private Random random = new Random(); // For random selection



        public RandomForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string filePath in openFileDialog.FileNames)
                    {
                        if (!imagePaths.Contains(filePath))
                        {
                            imagePaths.Add(filePath);
                            listBox1.Items.Add(System.IO.Path.GetFileName(filePath)); // Add filename to listBox1
                        }
                    }
                }
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                MessageBox.Show("Please add images first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Select a random image
            int randomIndex = random.Next(imagePaths.Count);
            string selectedImage = imagePaths[randomIndex];

            // Load the image and fix its orientation
            Image imageToDisplay = FixImageOrientation(Image.FromFile(selectedImage));
            pictureBox1.Image = imageToDisplay;

            // Move the selected image to "Used Images"
            usedImagePaths.Add(selectedImage);
            listBox2.Items.Add(System.IO.Path.GetFileName(selectedImage));

            imagePaths.RemoveAt(randomIndex);
            listBox1.Items.RemoveAt(randomIndex);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (usedImagePaths.Count == 0)
            {
                MessageBox.Show("No images to move back!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (string filePath in usedImagePaths)
            {
                imagePaths.Add(filePath);
                listBox1.Items.Add(System.IO.Path.GetFileName(filePath));
            }

            usedImagePaths.Clear();
            listBox2.Items.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Image FixImageOrientation(Image img)
        {
            const int ExifOrientationId = 0x112; // EXIF tag for Orientation

            try
            {
                if (img.PropertyIdList.Contains(ExifOrientationId))
                {
                    var prop = img.GetPropertyItem(ExifOrientationId);
                    int orientation = BitConverter.ToUInt16(prop.Value, 0);

                    // Apply necessary rotation based on orientation value
                    switch (orientation)
                    {
                        case 3: // 180 degrees
                            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 6: // 90 degrees clockwise
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 8: // 90 degrees counterclockwise
                            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }

                    // Remove the EXIF orientation tag to prevent reapplying rotation
                    img.RemovePropertyItem(ExifOrientationId);
                }
            }
            catch
            {
                // If something goes wrong, we just return the image as is.
            }

            return img;
        }


    }
}
