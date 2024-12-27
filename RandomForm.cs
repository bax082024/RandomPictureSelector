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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the data being dragged is a file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Allow copying
            }
            else
            {
                e.Effect = DragDropEffects.None; // Disallow if not files
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                // Check if the file is an image
                if (file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png") || file.EndsWith(".bmp"))
                {
                    if (!imagePaths.Contains(file))
                    {
                        imagePaths.Add(file);
                        listBox1.Items.Add(System.IO.Path.GetFileName(file));
                    }
                }
                else
                {
                    MessageBox.Show($"The file '{System.IO.Path.GetFileName(file)}' is not a supported image type.",
                                    "Invalid File",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt";
                saveFileDialog.Title = "Save Image Lists";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        // Save listBox1 (Insert Images)
                        writer.WriteLine("[Insert Images]");
                        foreach (string path in imagePaths)
                        {
                            writer.WriteLine(path);
                        }

                        // Save listBox2 (Used Images)
                        writer.WriteLine("[Used Images]");
                        foreach (string path in usedImagePaths)
                        {
                            writer.WriteLine(path);
                        }
                    }

                    MessageBox.Show("Image lists saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files|*.txt";
                openFileDialog.Title = "Load Image List";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] loadedPaths = System.IO.File.ReadAllLines(openFileDialog.FileName);

                    foreach (string path in loadedPaths)
                    {
                        if (!imagePaths.Contains(path) && System.IO.File.Exists(path))
                        {
                            imagePaths.Add(path);
                            listBox1.Items.Add(System.IO.Path.GetFileName(path));
                        }
                    }

                    MessageBox.Show("Image list loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
