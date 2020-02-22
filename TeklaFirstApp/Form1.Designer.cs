namespace TeklaFirstApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.openDrawings = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.createTopView = new System.Windows.Forms.CheckBox();
            this.createFrontView = new System.Windows.Forms.CheckBox();
            this.createEndView = new System.Windows.Forms.CheckBox();
            this.create3dView = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // openDrawings
            // 
            this.openDrawings.AutoSize = true;
            this.openDrawings.Location = new System.Drawing.Point(12, 68);
            this.openDrawings.Name = "openDrawings";
            this.openDrawings.Size = new System.Drawing.Size(99, 17);
            this.openDrawings.TabIndex = 1;
            this.openDrawings.Text = "Open Drawings";
            this.openDrawings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.create3dView);
            this.groupBox1.Controls.Add(this.createEndView);
            this.groupBox1.Controls.Add(this.createFrontView);
            this.groupBox1.Controls.Add(this.createTopView);
            this.groupBox1.Location = new System.Drawing.Point(117, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 111);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Views to be created";
            // 
            // createTopView
            // 
            this.createTopView.AutoSize = true;
            this.createTopView.Location = new System.Drawing.Point(6, 19);
            this.createTopView.Name = "createTopView";
            this.createTopView.Size = new System.Drawing.Size(68, 17);
            this.createTopView.TabIndex = 0;
            this.createTopView.Text = "TopView";
            this.createTopView.UseVisualStyleBackColor = true;
            // 
            // createFrontView
            // 
            this.createFrontView.AutoSize = true;
            this.createFrontView.Location = new System.Drawing.Point(6, 42);
            this.createFrontView.Name = "createFrontView";
            this.createFrontView.Size = new System.Drawing.Size(73, 17);
            this.createFrontView.TabIndex = 1;
            this.createFrontView.Text = "FrontView";
            this.createFrontView.UseVisualStyleBackColor = true;
            // 
            // createEndView
            // 
            this.createEndView.AutoSize = true;
            this.createEndView.Location = new System.Drawing.Point(6, 65);
            this.createEndView.Name = "createEndView";
            this.createEndView.Size = new System.Drawing.Size(68, 17);
            this.createEndView.TabIndex = 2;
            this.createEndView.Text = "EndView";
            this.createEndView.UseVisualStyleBackColor = true;
            // 
            // create3dView
            // 
            this.create3dView.AutoSize = true;
            this.create3dView.Location = new System.Drawing.Point(6, 88);
            this.create3dView.Name = "create3dView";
            this.create3dView.Size = new System.Drawing.Size(63, 17);
            this.create3dView.TabIndex = 3;
            this.create3dView.Text = "3DView";
            this.create3dView.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 151);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.openDrawings);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "BasicView";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox openDrawings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox create3dView;
        private System.Windows.Forms.CheckBox createEndView;
        private System.Windows.Forms.CheckBox createFrontView;
        private System.Windows.Forms.CheckBox createTopView;
    }
}

