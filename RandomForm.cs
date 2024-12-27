using RandomPictureSelector.UI;
using System.Diagnostics;

namespace RandomPictureSelector
{
    public partial class RandomForm : Form
    {
        private List<string> imagePaths = new List<string>(); // Paths for listBox1
        private List<string> usedImagePaths = new List<string>(); // Paths for listBox2
        private Random random = new Random(); // For random selection

        // Shuffle
        private int shuffleIndex = 0; // Tracks the current image being shown
        private int shuffleCount = 0; // Counts how many times images have been shuffled
        private int customShuffleSpeed = 100;// How many images to show during shuffle
        private int maxShuffleCount;

        private const string SettingsFilePath = "UserSettings.json";

        private GradientType currentGradient = GradientType.LightBlueToDarkBlue;
        private string currentTheme = "Light";




        public RandomForm()
        {
            InitializeComponent();
            LoadSettings();

            shuffleTimer.Tick += shuffleTimer_Tick;

            // Apply default theme if no settings are loaded
            if (string.IsNullOrEmpty(currentTheme))
            {
                currentTheme = "DesignView";
                ApplyTheme(currentTheme);
            }

            SaveSettings();
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

            // Reset shuffle variables
            shuffleIndex = 0;
            shuffleCount = 0;

            // Dynamically set MaxShuffleCount
            maxShuffleCount = Math.Max(20, Math.Min(imagePaths.Count, shuffleProgressBar.Maximum)); // Minimum 20 or the number of images
            shuffleProgressBar.Maximum = maxShuffleCount;

            // Reset progress bar
            ResetProgressBar(maxShuffleCount);

            // Update the shuffle label
            lblShuffleStatus.Text = $"Shuffling... 0/{maxShuffleCount}";

            // Show the first image immediately
            if (imagePaths.Count > 0)
            {
                pictureBox1.Image = FixImageOrientation(LoadImageSafely(imagePaths[shuffleIndex]));
                shuffleProgressBar.Value = 1; // Start progress bar at 1
            }

            // Start the shuffle timer
            shuffleTimer.Interval = customShuffleSpeed;
            shuffleTimer.Start();
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

                    if (prop?.Value != null)
                    {
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
                openFileDialog.Title = "Load Image Lists";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePaths.Clear();
                    usedImagePaths.Clear();
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();

                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        string line;
                        bool loadingInsertImages = false;
                        bool loadingUsedImages = false;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line == "[Insert Images]")
                            {
                                loadingInsertImages = true;
                                loadingUsedImages = false;
                                continue;
                            }

                            if (line == "[Used Images]")
                            {
                                loadingInsertImages = false;
                                loadingUsedImages = true;
                                continue;
                            }

                            if (loadingInsertImages)
                            {
                                if (System.IO.File.Exists(line))
                                {
                                    imagePaths.Add(line);
                                    listBox1.Items.Add(System.IO.Path.GetFileName(line));
                                }
                            }
                            else if (loadingUsedImages)
                            {
                                if (System.IO.File.Exists(line))
                                {
                                    usedImagePaths.Add(line);
                                    listBox2.Items.Add(System.IO.Path.GetFileName(line));
                                }
                            }
                        }
                    }

                    MessageBox.Show("Image lists loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void shuffleTimer_Tick(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                shuffleTimer.Stop();
                shuffleProgressBar.Visible = false;
                lblShuffleStatus.Text = "No images to shuffle!";
                return;
            }

            // Display the next image in the shuffle
            pictureBox1.Image = FixImageOrientation(LoadImageSafely(imagePaths[shuffleIndex]));

            // Cycle through the images
            shuffleIndex = (shuffleIndex + 1) % imagePaths.Count;

            // Increment shuffle counter and update progress bar
            shuffleCount++;
            shuffleProgressBar.Value = Math.Min(shuffleCount, maxShuffleCount);
            lblShuffleStatus.Text = $"Shuffling... {shuffleCount}/{maxShuffleCount}";

            // Stop after reaching the maximum shuffle count
            if (shuffleCount >= maxShuffleCount)
            {
                shuffleTimer.Stop();
                shuffleProgressBar.Value = shuffleProgressBar.Maximum;
                shuffleProgressBar.Visible = false;
                lblShuffleStatus.Text = "Shuffle complete!";
                SelectFinalRandomImage();
            }
        }









        private void SelectFinalRandomImage()
        {
            if (imagePaths.Count == 0)
            {
                lblShuffleStatus.Text = "No images to display!";
                return;
            }

            // Select a random image
            int randomIndex = random.Next(imagePaths.Count);
            string selectedImage = imagePaths[randomIndex];

            // Display the selected image in the PictureBox, fixing its orientation
            pictureBox1.Image = FixImageOrientation(Image.FromFile(selectedImage));

            // Move the selected image to "Used Images"
            usedImagePaths.Add(selectedImage);
            listBox2.Items.Add(System.IO.Path.GetFileName(selectedImage));

            imagePaths.RemoveAt(randomIndex);
            listBox1.Items.RemoveAt(randomIndex);

            lblShuffleStatus.Text = "Final image selected!";
        }


        private Image LoadImageSafely(string filePath)
        {
            try
            {
                return Image.FromFile(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void shuffleSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                // Pass current values to the SettingsForm
                settingsForm.MinShuffleCount = shuffleProgressBar.Minimum;
                settingsForm.ShuffleSpeed = customShuffleSpeed;

                // Show the SettingsForm
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Retrieve updated settings with validation
                    if (settingsForm.MinShuffleCount >= 1 && settingsForm.ShuffleSpeed > 0)
                    {
                        shuffleProgressBar.Minimum = settingsForm.MinShuffleCount;
                        customShuffleSpeed = settingsForm.ShuffleSpeed;
                    }
                    else
                    {
                        MessageBox.Show("Invalid settings. Please ensure values are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void ResetProgressBar(int calculatedShuffleCount)
        {
            shuffleProgressBar.Minimum = 0;
            shuffleProgressBar.Maximum = calculatedShuffleCount;
            shuffleProgressBar.Value = 0;
            shuffleProgressBar.Visible = true;
        }

        private void SaveSettings()
        {
            try
            {
                var settings = new UserSettings
                {
                    MinShuffleCount = shuffleProgressBar.Minimum,
                    ShuffleSpeed = customShuffleSpeed,
                    SelectedTheme = currentTheme
                };

                string json = System.Text.Json.JsonSerializer.Serialize(settings, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true // Make the JSON more readable
                });

                File.WriteAllText(SettingsFilePath, json);
                Debug.WriteLine("Settings saved successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to save settings: {ex.Message}");
            }
        }



        private void LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    var settings = System.Text.Json.JsonSerializer.Deserialize<UserSettings>(json);

                    if (settings != null)
                    {
                        shuffleProgressBar.Minimum = settings.MinShuffleCount;
                        customShuffleSpeed = settings.ShuffleSpeed;
                        currentTheme = settings.SelectedTheme;
                        ApplyTheme(currentTheme);
                        // Optional: Add a log message instead of showing a popup
                        Debug.WriteLine("Settings loaded successfully.");
                    }
                }
                else
                {
                    Debug.WriteLine("No settings file found. Default settings will be used.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load settings: {ex.Message}");
            }
        }



        private void ApplyTheme(string theme)
        {
            // Detach any previously attached Paint event for gradients
            this.Paint -= RandomForm_Paint;

            // Initialize colors dynamically based on the theme
            Color backgroundColor;
            Color textColor;
            Color buttonBackgroundColor;
            Color buttonTextColor;
            Color listBoxBackgroundColor;
            Color listBoxTextColor;
            Color menuStripBackgroundColor;
            Color menuStripTextColor;

            switch (theme)
            {
                case "Light":
                    backgroundColor = Color.White;
                    textColor = Color.Black;
                    buttonBackgroundColor = Color.LightGray;
                    buttonTextColor = Color.Black;
                    listBoxBackgroundColor = Color.White;
                    listBoxTextColor = Color.Black;
                    menuStripBackgroundColor = Color.White;
                    menuStripTextColor = Color.Black;
                    break;

                case "Dark":
                    backgroundColor = Color.Black;
                    textColor = Color.White;
                    buttonBackgroundColor = Color.Gray;
                    buttonTextColor = Color.White;
                    listBoxBackgroundColor = Color.Black;
                    listBoxTextColor = Color.White;
                    menuStripBackgroundColor = Color.Black;
                    menuStripTextColor = Color.White;
                    break;

                case "Gradient":
                    // Attach Paint event for gradient background
                    this.Paint += RandomForm_Paint;

                    backgroundColor = Color.Transparent; // Gradient is drawn in Paint event
                    textColor = Color.White;
                    buttonBackgroundColor = Color.DarkGray;
                    buttonTextColor = Color.White;
                    listBoxBackgroundColor = Color.Gray;
                    listBoxTextColor = Color.White;
                    menuStripBackgroundColor = Color.DarkGray;
                    menuStripTextColor = Color.White;
                    break;

                case "DesignView":
                default:
                    backgroundColor = Color.LightYellow;
                    textColor = Color.Black;
                    buttonBackgroundColor = Color.LightGoldenrodYellow;
                    buttonTextColor = Color.Black;
                    listBoxBackgroundColor = Color.LightYellow;
                    listBoxTextColor = Color.Black;
                    menuStripBackgroundColor = Color.Beige;
                    menuStripTextColor = Color.Black;
                    break;
            }

            // Apply colors to the form
            this.BackColor = theme == "Gradient" ? Color.White : backgroundColor;
            this.ForeColor = textColor;

            // Iterate through all controls and apply colors dynamically
            foreach (Control control in this.Controls)
            {
                switch (control)
                {
                    case Button button:
                        button.BackColor = buttonBackgroundColor;
                        button.ForeColor = buttonTextColor;
                        break;

                    case ListBox listBox:
                        listBox.BackColor = listBoxBackgroundColor;
                        listBox.ForeColor = listBoxTextColor;
                        break;

                    case MenuStrip menuStrip:
                        menuStrip.BackColor = menuStripBackgroundColor;
                        menuStrip.ForeColor = menuStripTextColor;
                        break;

                    case Label label:
                        label.ForeColor = textColor; // Apply text color to labels
                        break;

                    case ProgressBar progressBar:
                        progressBar.BackColor = backgroundColor; // Match form background
                        progressBar.ForeColor = textColor;       // Optional
                        break;

                    default:
                        // Apply default colors for other controls
                        control.BackColor = backgroundColor;
                        control.ForeColor = textColor;
                        break;
                }
            }

            // Save the applied theme to settings
            SaveSettings();
        }



        // Paint event for the gradient background
        private void RandomForm_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.LinearGradientBrush gradientBrush;

            switch (currentGradient)
            {
                case GradientType.OrangeToYellow:
                    gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        this.ClientRectangle,
                        Color.Orange,
                        Color.Yellow,
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    break;

                case GradientType.BlueToPurple:
                    gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        this.ClientRectangle,
                        Color.Blue,
                        Color.Purple,
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    break;

                case GradientType.LightBlueToDarkBlue:
                default:
                    gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        this.ClientRectangle,
                        Color.LightBlue,
                        Color.DarkBlue,
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    break;
            }

            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
            gradientBrush.Dispose();
        }



        public enum GradientType
        {
            LightBlueToDarkBlue,
            OrangeToYellow,
            BlueToPurple
        }

        private void ChangeTheme(string theme)
        {
            currentTheme = theme; // Update the current theme
            ApplyTheme(theme); // Apply the selected theme
            SaveSettings(); // Save the selected theme in settings
        }

        private void saveSettiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void RandomForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Light");
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Dark");
        }

        private void gradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Gradient");
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("DesignView");
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentGradient = GradientType.LightBlueToDarkBlue;
            ApplyTheme("Gradient");
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentGradient = GradientType.OrangeToYellow;
            ApplyTheme("Gradient");
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentGradient = GradientType.BlueToPurple;
            ApplyTheme("Gradient");
        }
    }
}
