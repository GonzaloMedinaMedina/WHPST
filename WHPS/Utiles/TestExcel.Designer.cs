namespace WHPS
{
    partial class TestExcel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlParametros = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSelect = new System.Windows.Forms.TabPage();
            this.gbSelectFiltro = new System.Windows.Forms.GroupBox();
            this.dgvSelectFiltro = new System.Windows.Forms.DataGridView();
            this.ColSelectFiltroTipo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColSelectFiltroNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSelectFiltroOperador = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColSelectFiltroValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbSelectSalidaError = new System.Windows.Forms.TextBox();
            this.tbSelectNombreColumnas = new System.Windows.Forms.TextBox();
            this.lblSelectNombreColumnas = new System.Windows.Forms.Label();
            this.tbSelectClaveMaquina = new System.Windows.Forms.TextBox();
            this.lblSelectClaveMaquina = new System.Windows.Forms.Label();
            this.tbSelectNombreHoja = new System.Windows.Forms.TextBox();
            this.lblSelectNombreHoja = new System.Windows.Forms.Label();
            this.cbEjecutarSelect = new System.Windows.Forms.Button();
            this.tabUpdate = new System.Windows.Forms.TabPage();
            this.pnlUpdateFiltro = new System.Windows.Forms.Panel();
            this.gbUpdateFiltro = new System.Windows.Forms.GroupBox();
            this.dgvUpdateFiltro = new System.Windows.Forms.DataGridView();
            this.ColUpdateFiltroTipo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColUpdateFiltroNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUpdateFiltroOperador = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColUpdateFiltroValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlUpdateValores = new System.Windows.Forms.Panel();
            this.gbUpdateValores = new System.Windows.Forms.GroupBox();
            this.dgvUpdateValores = new System.Windows.Forms.DataGridView();
            this.ColUpdateValoresNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUpdateValoresValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbUpdateSalida = new System.Windows.Forms.TextBox();
            this.tbUpdateClaveMaquina = new System.Windows.Forms.TextBox();
            this.lblUpdateClaveMaquina = new System.Windows.Forms.Label();
            this.tbUpdateNombreHoja = new System.Windows.Forms.TextBox();
            this.lblUpdateNombreHoja = new System.Windows.Forms.Label();
            this.cbUpdateEjecutar = new System.Windows.Forms.Button();
            this.tabInsert = new System.Windows.Forms.TabPage();
            this.tbInsertIdentificador = new System.Windows.Forms.TextBox();
            this.lblInsertIdentificador = new System.Windows.Forms.Label();
            this.gbInsertValores = new System.Windows.Forms.GroupBox();
            this.dgvInsertValores = new System.Windows.Forms.DataGridView();
            this.ColInsertValoresNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColInsertValoresValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbInsertSalida = new System.Windows.Forms.TextBox();
            this.tbInsertClaveMaquina = new System.Windows.Forms.TextBox();
            this.lblInsertClaveMaquina = new System.Windows.Forms.Label();
            this.tbInsertNombreHoja = new System.Windows.Forms.TextBox();
            this.lblInsertNombreHoja = new System.Windows.Forms.Label();
            this.cbInsertEjecutar = new System.Windows.Forms.Button();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.dgvSalidaConsulta = new System.Windows.Forms.DataGridView();
            this.dsSelectFiltro = new System.Data.DataSet();
            this.pnlParametros.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabSelect.SuspendLayout();
            this.gbSelectFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectFiltro)).BeginInit();
            this.tabUpdate.SuspendLayout();
            this.pnlUpdateFiltro.SuspendLayout();
            this.gbUpdateFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateFiltro)).BeginInit();
            this.pnlUpdateValores.SuspendLayout();
            this.gbUpdateValores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateValores)).BeginInit();
            this.tabInsert.SuspendLayout();
            this.gbInsertValores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsertValores)).BeginInit();
            this.pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalidaConsulta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSelectFiltro)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlParametros
            // 
            this.pnlParametros.Controls.Add(this.tabControl);
            this.pnlParametros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlParametros.Location = new System.Drawing.Point(0, 0);
            this.pnlParametros.Name = "pnlParametros";
            this.pnlParametros.Size = new System.Drawing.Size(1021, 190);
            this.pnlParametros.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSelect);
            this.tabControl.Controls.Add(this.tabUpdate);
            this.tabControl.Controls.Add(this.tabInsert);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1021, 190);
            this.tabControl.TabIndex = 0;
            // 
            // tabSelect
            // 
            this.tabSelect.Controls.Add(this.gbSelectFiltro);
            this.tabSelect.Controls.Add(this.tbSelectSalidaError);
            this.tabSelect.Controls.Add(this.tbSelectNombreColumnas);
            this.tabSelect.Controls.Add(this.lblSelectNombreColumnas);
            this.tabSelect.Controls.Add(this.tbSelectClaveMaquina);
            this.tabSelect.Controls.Add(this.lblSelectClaveMaquina);
            this.tabSelect.Controls.Add(this.tbSelectNombreHoja);
            this.tabSelect.Controls.Add(this.lblSelectNombreHoja);
            this.tabSelect.Controls.Add(this.cbEjecutarSelect);
            this.tabSelect.Location = new System.Drawing.Point(4, 22);
            this.tabSelect.Name = "tabSelect";
            this.tabSelect.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabSelect.Size = new System.Drawing.Size(1013, 164);
            this.tabSelect.TabIndex = 0;
            this.tabSelect.Text = "SELECT";
            this.tabSelect.UseVisualStyleBackColor = true;
            // 
            // gbSelectFiltro
            // 
            this.gbSelectFiltro.Controls.Add(this.dgvSelectFiltro);
            this.gbSelectFiltro.Location = new System.Drawing.Point(460, 9);
            this.gbSelectFiltro.Name = "gbSelectFiltro";
            this.gbSelectFiltro.Size = new System.Drawing.Size(390, 149);
            this.gbSelectFiltro.TabIndex = 8;
            this.gbSelectFiltro.TabStop = false;
            this.gbSelectFiltro.Text = "Filtro";
            // 
            // dgvSelectFiltro
            // 
            this.dgvSelectFiltro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectFiltro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColSelectFiltroTipo,
            this.ColSelectFiltroNombre,
            this.ColSelectFiltroOperador,
            this.ColSelectFiltroValor});
            this.dgvSelectFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelectFiltro.Location = new System.Drawing.Point(3, 16);
            this.dgvSelectFiltro.Name = "dgvSelectFiltro";
            this.dgvSelectFiltro.Size = new System.Drawing.Size(384, 130);
            this.dgvSelectFiltro.TabIndex = 5;
            // 
            // ColSelectFiltroTipo
            // 
            this.ColSelectFiltroTipo.HeaderText = "Tipo";
            this.ColSelectFiltroTipo.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.ColSelectFiltroTipo.Name = "ColSelectFiltroTipo";
            this.ColSelectFiltroTipo.Width = 60;
            // 
            // ColSelectFiltroNombre
            // 
            this.ColSelectFiltroNombre.HeaderText = "Nombre";
            this.ColSelectFiltroNombre.Name = "ColSelectFiltroNombre";
            // 
            // ColSelectFiltroOperador
            // 
            this.ColSelectFiltroOperador.HeaderText = "Operador";
            this.ColSelectFiltroOperador.Items.AddRange(new object[] {
            "=",
            "LIKE",
            ">",
            "<"});
            this.ColSelectFiltroOperador.Name = "ColSelectFiltroOperador";
            this.ColSelectFiltroOperador.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColSelectFiltroOperador.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColSelectFiltroOperador.Width = 80;
            // 
            // ColSelectFiltroValor
            // 
            this.ColSelectFiltroValor.HeaderText = "Valor";
            this.ColSelectFiltroValor.Name = "ColSelectFiltroValor";
            // 
            // tbSelectSalidaError
            // 
            this.tbSelectSalidaError.Location = new System.Drawing.Point(11, 105);
            this.tbSelectSalidaError.Multiline = true;
            this.tbSelectSalidaError.Name = "tbSelectSalidaError";
            this.tbSelectSalidaError.ReadOnly = true;
            this.tbSelectSalidaError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSelectSalidaError.Size = new System.Drawing.Size(443, 53);
            this.tbSelectSalidaError.TabIndex = 7;
            // 
            // tbSelectNombreColumnas
            // 
            this.tbSelectNombreColumnas.Location = new System.Drawing.Point(110, 72);
            this.tbSelectNombreColumnas.Name = "tbSelectNombreColumnas";
            this.tbSelectNombreColumnas.Size = new System.Drawing.Size(344, 20);
            this.tbSelectNombreColumnas.TabIndex = 2;
            // 
            // lblSelectNombreColumnas
            // 
            this.lblSelectNombreColumnas.AutoSize = true;
            this.lblSelectNombreColumnas.Location = new System.Drawing.Point(8, 75);
            this.lblSelectNombreColumnas.Name = "lblSelectNombreColumnas";
            this.lblSelectNombreColumnas.Size = new System.Drawing.Size(93, 13);
            this.lblSelectNombreColumnas.TabIndex = 5;
            this.lblSelectNombreColumnas.Text = "Nombre Columnas";
            // 
            // tbSelectClaveMaquina
            // 
            this.tbSelectClaveMaquina.Location = new System.Drawing.Point(110, 9);
            this.tbSelectClaveMaquina.Name = "tbSelectClaveMaquina";
            this.tbSelectClaveMaquina.Size = new System.Drawing.Size(126, 20);
            this.tbSelectClaveMaquina.TabIndex = 0;
            // 
            // lblSelectClaveMaquina
            // 
            this.lblSelectClaveMaquina.AutoSize = true;
            this.lblSelectClaveMaquina.Location = new System.Drawing.Point(8, 12);
            this.lblSelectClaveMaquina.Name = "lblSelectClaveMaquina";
            this.lblSelectClaveMaquina.Size = new System.Drawing.Size(78, 13);
            this.lblSelectClaveMaquina.TabIndex = 3;
            this.lblSelectClaveMaquina.Text = "Clave Máquina";
            // 
            // tbSelectNombreHoja
            // 
            this.tbSelectNombreHoja.Location = new System.Drawing.Point(110, 38);
            this.tbSelectNombreHoja.Name = "tbSelectNombreHoja";
            this.tbSelectNombreHoja.Size = new System.Drawing.Size(126, 20);
            this.tbSelectNombreHoja.TabIndex = 1;
            // 
            // lblSelectNombreHoja
            // 
            this.lblSelectNombreHoja.AutoSize = true;
            this.lblSelectNombreHoja.Location = new System.Drawing.Point(8, 41);
            this.lblSelectNombreHoja.Name = "lblSelectNombreHoja";
            this.lblSelectNombreHoja.Size = new System.Drawing.Size(84, 13);
            this.lblSelectNombreHoja.TabIndex = 1;
            this.lblSelectNombreHoja.Text = "Nombre de Hoja";
            // 
            // cbEjecutarSelect
            // 
            this.cbEjecutarSelect.Location = new System.Drawing.Point(892, 124);
            this.cbEjecutarSelect.Name = "cbEjecutarSelect";
            this.cbEjecutarSelect.Size = new System.Drawing.Size(75, 23);
            this.cbEjecutarSelect.TabIndex = 3;
            this.cbEjecutarSelect.Text = "Ejecutar";
            this.cbEjecutarSelect.UseVisualStyleBackColor = true;
            this.cbEjecutarSelect.Click += new System.EventHandler(this.cbEjecutarSelect_Click);
            // 
            // tabUpdate
            // 
            this.tabUpdate.Controls.Add(this.pnlUpdateFiltro);
            this.tabUpdate.Controls.Add(this.pnlUpdateValores);
            this.tabUpdate.Controls.Add(this.tbUpdateSalida);
            this.tabUpdate.Controls.Add(this.tbUpdateClaveMaquina);
            this.tabUpdate.Controls.Add(this.lblUpdateClaveMaquina);
            this.tabUpdate.Controls.Add(this.tbUpdateNombreHoja);
            this.tabUpdate.Controls.Add(this.lblUpdateNombreHoja);
            this.tabUpdate.Controls.Add(this.cbUpdateEjecutar);
            this.tabUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabUpdate.Name = "tabUpdate";
            this.tabUpdate.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabUpdate.Size = new System.Drawing.Size(1013, 164);
            this.tabUpdate.TabIndex = 1;
            this.tabUpdate.Text = "UPDATE";
            this.tabUpdate.UseVisualStyleBackColor = true;
            // 
            // pnlUpdateFiltro
            // 
            this.pnlUpdateFiltro.Controls.Add(this.gbUpdateFiltro);
            this.pnlUpdateFiltro.Location = new System.Drawing.Point(515, 7);
            this.pnlUpdateFiltro.Name = "pnlUpdateFiltro";
            this.pnlUpdateFiltro.Size = new System.Drawing.Size(390, 155);
            this.pnlUpdateFiltro.TabIndex = 18;
            // 
            // gbUpdateFiltro
            // 
            this.gbUpdateFiltro.Controls.Add(this.dgvUpdateFiltro);
            this.gbUpdateFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUpdateFiltro.Location = new System.Drawing.Point(0, 0);
            this.gbUpdateFiltro.Name = "gbUpdateFiltro";
            this.gbUpdateFiltro.Size = new System.Drawing.Size(390, 155);
            this.gbUpdateFiltro.TabIndex = 9;
            this.gbUpdateFiltro.TabStop = false;
            this.gbUpdateFiltro.Text = "Filtro";
            // 
            // dgvUpdateFiltro
            // 
            this.dgvUpdateFiltro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpdateFiltro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColUpdateFiltroTipo,
            this.ColUpdateFiltroNombre,
            this.ColUpdateFiltroOperador,
            this.ColUpdateFiltroValor});
            this.dgvUpdateFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUpdateFiltro.Location = new System.Drawing.Point(3, 16);
            this.dgvUpdateFiltro.Name = "dgvUpdateFiltro";
            this.dgvUpdateFiltro.Size = new System.Drawing.Size(384, 136);
            this.dgvUpdateFiltro.TabIndex = 0;
            // 
            // ColUpdateFiltroTipo
            // 
            this.ColUpdateFiltroTipo.HeaderText = "Tipo";
            this.ColUpdateFiltroTipo.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.ColUpdateFiltroTipo.Name = "ColUpdateFiltroTipo";
            this.ColUpdateFiltroTipo.Width = 60;
            // 
            // ColUpdateFiltroNombre
            // 
            this.ColUpdateFiltroNombre.HeaderText = "Nombre";
            this.ColUpdateFiltroNombre.Name = "ColUpdateFiltroNombre";
            // 
            // ColUpdateFiltroOperador
            // 
            this.ColUpdateFiltroOperador.HeaderText = "Operador";
            this.ColUpdateFiltroOperador.Items.AddRange(new object[] {
            "=",
            "LIKE",
            "<",
            ">"});
            this.ColUpdateFiltroOperador.Name = "ColUpdateFiltroOperador";
            this.ColUpdateFiltroOperador.Width = 80;
            // 
            // ColUpdateFiltroValor
            // 
            this.ColUpdateFiltroValor.HeaderText = "Valor";
            this.ColUpdateFiltroValor.Name = "ColUpdateFiltroValor";
            // 
            // pnlUpdateValores
            // 
            this.pnlUpdateValores.Controls.Add(this.gbUpdateValores);
            this.pnlUpdateValores.Location = new System.Drawing.Point(262, 7);
            this.pnlUpdateValores.Name = "pnlUpdateValores";
            this.pnlUpdateValores.Size = new System.Drawing.Size(249, 155);
            this.pnlUpdateValores.TabIndex = 17;
            // 
            // gbUpdateValores
            // 
            this.gbUpdateValores.Controls.Add(this.dgvUpdateValores);
            this.gbUpdateValores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUpdateValores.Location = new System.Drawing.Point(0, 0);
            this.gbUpdateValores.Name = "gbUpdateValores";
            this.gbUpdateValores.Size = new System.Drawing.Size(249, 155);
            this.gbUpdateValores.TabIndex = 9;
            this.gbUpdateValores.TabStop = false;
            this.gbUpdateValores.Text = "Valores";
            // 
            // dgvUpdateValores
            // 
            this.dgvUpdateValores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpdateValores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColUpdateValoresNombre,
            this.ColUpdateValoresValor});
            this.dgvUpdateValores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUpdateValores.Location = new System.Drawing.Point(3, 16);
            this.dgvUpdateValores.Name = "dgvUpdateValores";
            this.dgvUpdateValores.Size = new System.Drawing.Size(243, 136);
            this.dgvUpdateValores.TabIndex = 0;
            // 
            // ColUpdateValoresNombre
            // 
            this.ColUpdateValoresNombre.HeaderText = "Nombre";
            this.ColUpdateValoresNombre.Name = "ColUpdateValoresNombre";
            // 
            // ColUpdateValoresValor
            // 
            this.ColUpdateValoresValor.HeaderText = "Valor";
            this.ColUpdateValoresValor.Name = "ColUpdateValoresValor";
            // 
            // tbUpdateSalida
            // 
            this.tbUpdateSalida.Location = new System.Drawing.Point(30, 62);
            this.tbUpdateSalida.Multiline = true;
            this.tbUpdateSalida.Name = "tbUpdateSalida";
            this.tbUpdateSalida.ReadOnly = true;
            this.tbUpdateSalida.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUpdateSalida.Size = new System.Drawing.Size(225, 88);
            this.tbUpdateSalida.TabIndex = 16;
            // 
            // tbUpdateClaveMaquina
            // 
            this.tbUpdateClaveMaquina.Location = new System.Drawing.Point(129, 7);
            this.tbUpdateClaveMaquina.Name = "tbUpdateClaveMaquina";
            this.tbUpdateClaveMaquina.Size = new System.Drawing.Size(126, 20);
            this.tbUpdateClaveMaquina.TabIndex = 0;
            // 
            // lblUpdateClaveMaquina
            // 
            this.lblUpdateClaveMaquina.AutoSize = true;
            this.lblUpdateClaveMaquina.Location = new System.Drawing.Point(27, 11);
            this.lblUpdateClaveMaquina.Name = "lblUpdateClaveMaquina";
            this.lblUpdateClaveMaquina.Size = new System.Drawing.Size(78, 13);
            this.lblUpdateClaveMaquina.TabIndex = 12;
            this.lblUpdateClaveMaquina.Text = "Clave Máquina";
            // 
            // tbUpdateNombreHoja
            // 
            this.tbUpdateNombreHoja.Location = new System.Drawing.Point(129, 36);
            this.tbUpdateNombreHoja.Name = "tbUpdateNombreHoja";
            this.tbUpdateNombreHoja.Size = new System.Drawing.Size(126, 20);
            this.tbUpdateNombreHoja.TabIndex = 1;
            // 
            // lblUpdateNombreHoja
            // 
            this.lblUpdateNombreHoja.AutoSize = true;
            this.lblUpdateNombreHoja.Location = new System.Drawing.Point(27, 40);
            this.lblUpdateNombreHoja.Name = "lblUpdateNombreHoja";
            this.lblUpdateNombreHoja.Size = new System.Drawing.Size(84, 13);
            this.lblUpdateNombreHoja.TabIndex = 10;
            this.lblUpdateNombreHoja.Text = "Nombre de Hoja";
            // 
            // cbUpdateEjecutar
            // 
            this.cbUpdateEjecutar.Location = new System.Drawing.Point(925, 133);
            this.cbUpdateEjecutar.Name = "cbUpdateEjecutar";
            this.cbUpdateEjecutar.Size = new System.Drawing.Size(75, 23);
            this.cbUpdateEjecutar.TabIndex = 1;
            this.cbUpdateEjecutar.Text = "Ejecutar";
            this.cbUpdateEjecutar.UseVisualStyleBackColor = true;
            this.cbUpdateEjecutar.Click += new System.EventHandler(this.cbUpdateEjecutar_Click);
            // 
            // tabInsert
            // 
            this.tabInsert.Controls.Add(this.tbInsertIdentificador);
            this.tabInsert.Controls.Add(this.lblInsertIdentificador);
            this.tabInsert.Controls.Add(this.gbInsertValores);
            this.tabInsert.Controls.Add(this.tbInsertSalida);
            this.tabInsert.Controls.Add(this.tbInsertClaveMaquina);
            this.tabInsert.Controls.Add(this.lblInsertClaveMaquina);
            this.tabInsert.Controls.Add(this.tbInsertNombreHoja);
            this.tabInsert.Controls.Add(this.lblInsertNombreHoja);
            this.tabInsert.Controls.Add(this.cbInsertEjecutar);
            this.tabInsert.Location = new System.Drawing.Point(4, 22);
            this.tabInsert.Name = "tabInsert";
            this.tabInsert.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabInsert.Size = new System.Drawing.Size(1013, 164);
            this.tabInsert.TabIndex = 2;
            this.tabInsert.Text = "INSERT";
            this.tabInsert.UseVisualStyleBackColor = true;
            // 
            // tbInsertIdentificador
            // 
            this.tbInsertIdentificador.Location = new System.Drawing.Point(129, 64);
            this.tbInsertIdentificador.Name = "tbInsertIdentificador";
            this.tbInsertIdentificador.Size = new System.Drawing.Size(126, 20);
            this.tbInsertIdentificador.TabIndex = 2;
            // 
            // lblInsertIdentificador
            // 
            this.lblInsertIdentificador.AutoSize = true;
            this.lblInsertIdentificador.Location = new System.Drawing.Point(27, 68);
            this.lblInsertIdentificador.Name = "lblInsertIdentificador";
            this.lblInsertIdentificador.Size = new System.Drawing.Size(65, 13);
            this.lblInsertIdentificador.TabIndex = 18;
            this.lblInsertIdentificador.Text = "Identificador";
            // 
            // gbInsertValores
            // 
            this.gbInsertValores.Controls.Add(this.dgvInsertValores);
            this.gbInsertValores.Location = new System.Drawing.Point(418, 8);
            this.gbInsertValores.Name = "gbInsertValores";
            this.gbInsertValores.Size = new System.Drawing.Size(251, 149);
            this.gbInsertValores.TabIndex = 17;
            this.gbInsertValores.TabStop = false;
            this.gbInsertValores.Text = "Valores";
            // 
            // dgvInsertValores
            // 
            this.dgvInsertValores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsertValores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColInsertValoresNombre,
            this.ColInsertValoresValor});
            this.dgvInsertValores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInsertValores.Location = new System.Drawing.Point(3, 16);
            this.dgvInsertValores.Name = "dgvInsertValores";
            this.dgvInsertValores.Size = new System.Drawing.Size(245, 130);
            this.dgvInsertValores.TabIndex = 0;
            // 
            // ColInsertValoresNombre
            // 
            this.ColInsertValoresNombre.HeaderText = "Nombre";
            this.ColInsertValoresNombre.Name = "ColInsertValoresNombre";
            // 
            // ColInsertValoresValor
            // 
            this.ColInsertValoresValor.HeaderText = "Valor";
            this.ColInsertValoresValor.Name = "ColInsertValoresValor";
            // 
            // tbInsertSalida
            // 
            this.tbInsertSalida.Location = new System.Drawing.Point(30, 91);
            this.tbInsertSalida.Multiline = true;
            this.tbInsertSalida.Name = "tbInsertSalida";
            this.tbInsertSalida.ReadOnly = true;
            this.tbInsertSalida.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInsertSalida.Size = new System.Drawing.Size(382, 66);
            this.tbInsertSalida.TabIndex = 16;
            // 
            // tbInsertClaveMaquina
            // 
            this.tbInsertClaveMaquina.Location = new System.Drawing.Point(129, 8);
            this.tbInsertClaveMaquina.Name = "tbInsertClaveMaquina";
            this.tbInsertClaveMaquina.Size = new System.Drawing.Size(126, 20);
            this.tbInsertClaveMaquina.TabIndex = 0;
            // 
            // lblInsertClaveMaquina
            // 
            this.lblInsertClaveMaquina.AutoSize = true;
            this.lblInsertClaveMaquina.Location = new System.Drawing.Point(27, 12);
            this.lblInsertClaveMaquina.Name = "lblInsertClaveMaquina";
            this.lblInsertClaveMaquina.Size = new System.Drawing.Size(78, 13);
            this.lblInsertClaveMaquina.TabIndex = 12;
            this.lblInsertClaveMaquina.Text = "Clave Máquina";
            // 
            // tbInsertNombreHoja
            // 
            this.tbInsertNombreHoja.Location = new System.Drawing.Point(129, 37);
            this.tbInsertNombreHoja.Name = "tbInsertNombreHoja";
            this.tbInsertNombreHoja.Size = new System.Drawing.Size(126, 20);
            this.tbInsertNombreHoja.TabIndex = 1;
            // 
            // lblInsertNombreHoja
            // 
            this.lblInsertNombreHoja.AutoSize = true;
            this.lblInsertNombreHoja.Location = new System.Drawing.Point(27, 41);
            this.lblInsertNombreHoja.Name = "lblInsertNombreHoja";
            this.lblInsertNombreHoja.Size = new System.Drawing.Size(84, 13);
            this.lblInsertNombreHoja.TabIndex = 10;
            this.lblInsertNombreHoja.Text = "Nombre de Hoja";
            // 
            // cbInsertEjecutar
            // 
            this.cbInsertEjecutar.Location = new System.Drawing.Point(911, 123);
            this.cbInsertEjecutar.Name = "cbInsertEjecutar";
            this.cbInsertEjecutar.Size = new System.Drawing.Size(75, 23);
            this.cbInsertEjecutar.TabIndex = 3;
            this.cbInsertEjecutar.Text = "Ejecutar";
            this.cbInsertEjecutar.UseVisualStyleBackColor = true;
            this.cbInsertEjecutar.Click += new System.EventHandler(this.cbInsertEjecutar_Click);
            // 
            // pnlContenido
            // 
            this.pnlContenido.Controls.Add(this.dgvSalidaConsulta);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 190);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1021, 362);
            this.pnlContenido.TabIndex = 1;
            // 
            // dgvSalidaConsulta
            // 
            this.dgvSalidaConsulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalidaConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSalidaConsulta.Location = new System.Drawing.Point(0, 0);
            this.dgvSalidaConsulta.Name = "dgvSalidaConsulta";
            this.dgvSalidaConsulta.Size = new System.Drawing.Size(1021, 362);
            this.dgvSalidaConsulta.TabIndex = 0;
            // 
            // dsSelectFiltro
            // 
            this.dsSelectFiltro.DataSetName = "DataSelectFiltro";
            // 
            // TestExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 552);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlParametros);
            this.Name = "TestExcel";
            this.Text = "TestExcel";
            this.TopMost = true;
            this.pnlParametros.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabSelect.ResumeLayout(false);
            this.tabSelect.PerformLayout();
            this.gbSelectFiltro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectFiltro)).EndInit();
            this.tabUpdate.ResumeLayout(false);
            this.tabUpdate.PerformLayout();
            this.pnlUpdateFiltro.ResumeLayout(false);
            this.gbUpdateFiltro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateFiltro)).EndInit();
            this.pnlUpdateValores.ResumeLayout(false);
            this.gbUpdateValores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdateValores)).EndInit();
            this.tabInsert.ResumeLayout(false);
            this.tabInsert.PerformLayout();
            this.gbInsertValores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsertValores)).EndInit();
            this.pnlContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalidaConsulta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSelectFiltro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlParametros;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSelect;
        private System.Windows.Forms.Button cbEjecutarSelect;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.TabPage tabInsert;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.TextBox tbSelectNombreColumnas;
        private System.Windows.Forms.Label lblSelectNombreColumnas;
        private System.Windows.Forms.TextBox tbSelectClaveMaquina;
        private System.Windows.Forms.Label lblSelectClaveMaquina;
        private System.Windows.Forms.TextBox tbSelectNombreHoja;
        private System.Windows.Forms.Label lblSelectNombreHoja;
        private System.Windows.Forms.TextBox tbSelectSalidaError;
        private System.Windows.Forms.GroupBox gbSelectFiltro;
        private System.Windows.Forms.DataGridView dgvSelectFiltro;
        private System.Windows.Forms.Panel pnlUpdateFiltro;
        private System.Windows.Forms.GroupBox gbUpdateFiltro;
        private System.Windows.Forms.DataGridView dgvUpdateFiltro;
        private System.Windows.Forms.Panel pnlUpdateValores;
        private System.Windows.Forms.GroupBox gbUpdateValores;
        private System.Windows.Forms.DataGridView dgvUpdateValores;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUpdateValoresNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUpdateValoresValor;
        private System.Windows.Forms.TextBox tbUpdateSalida;
        private System.Windows.Forms.TextBox tbUpdateClaveMaquina;
        private System.Windows.Forms.Label lblUpdateClaveMaquina;
        private System.Windows.Forms.TextBox tbUpdateNombreHoja;
        private System.Windows.Forms.Label lblUpdateNombreHoja;
        private System.Windows.Forms.Button cbUpdateEjecutar;
        private System.Windows.Forms.GroupBox gbInsertValores;
        private System.Windows.Forms.DataGridView dgvInsertValores;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColInsertValoresNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColInsertValoresValor;
        private System.Windows.Forms.TextBox tbInsertSalida;
        private System.Windows.Forms.TextBox tbInsertClaveMaquina;
        private System.Windows.Forms.Label lblInsertClaveMaquina;
        private System.Windows.Forms.TextBox tbInsertNombreHoja;
        private System.Windows.Forms.Label lblInsertNombreHoja;
        private System.Windows.Forms.Button cbInsertEjecutar;
        private System.Windows.Forms.DataGridView dgvSalidaConsulta;
        private System.Data.DataSet dsSelectFiltro;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColSelectFiltroTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSelectFiltroNombre;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColSelectFiltroOperador;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSelectFiltroValor;
        private System.Windows.Forms.TextBox tbInsertIdentificador;
        private System.Windows.Forms.Label lblInsertIdentificador;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColUpdateFiltroTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUpdateFiltroNombre;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColUpdateFiltroOperador;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUpdateFiltroValor;
    }
}