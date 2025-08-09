using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogicLayer.Control
{
    public class DataGridViewCustomTextBoxColumn : DataGridViewColumn
    {
        public DataGridViewCustomTextBoxColumn()
            : base(new DataGridViewCustomTextBoxCell())
        { }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewCustomTextBoxCell)))
                {
                    throw new InvalidCastException("Must have value");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewCustomTextBoxCell : DataGridViewTextBoxCell
    {
        public DataGridViewCustomTextBoxCell() : base() { }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            CustomTextBoxEditingControl ctl = DataGridView.EditingControl as CustomTextBoxEditingControl;
            ctl.Text = (string)this.Value;
        }
        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(CustomTextBoxEditingControl);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return string.Empty;
            }
        }
    }

    class CustomTextBoxEditingControl : TextBox, IDataGridViewEditingControl
    {
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            //throw new NotImplementedException();
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }

        DataGridView _EditingControlDataGridView;
        public DataGridView EditingControlDataGridView
        {
            get
            {
                //throw new NotImplementedException();
                return _EditingControlDataGridView;
            }
            set
            {
                _EditingControlDataGridView = value;
                //throw new NotImplementedException();
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = (string)value;
            }
        }

        int _EditingControlRowIndex;
        public int EditingControlRowIndex
        {
            get
            {
                return _EditingControlRowIndex;
            }
            set
            {
                _EditingControlRowIndex = value;
            }
        }

        bool _TextBoxValueChanged = false;
        public bool EditingControlValueChanged
        {
            get
            {
                //throw new NotImplementedException();
                return _TextBoxValueChanged;
            }
            set
            {
                _TextBoxValueChanged = value;
                //throw new NotImplementedException();
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            //throw new NotImplementedException();
            //return keyData == Keys.Enter;
            return true;
        }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
            // throw new NotImplementedException();
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            //throw new NotImplementedException();
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
                //throw new NotImplementedException(); 
            }
        }


        protected override void OnTextChanged(EventArgs e)
        {
            _TextBoxValueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(e);
        }

    }
}
