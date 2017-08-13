using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScdMergeWizard.Formz
{
    public partial class TransformationHelpForm : Form
    {
        private MyTransformation _transformation;

        public TransformationHelpForm(MyTransformation transformation)
        {
            InitializeComponent();

            _transformation = transformation;

            initForm();
        }

        private void initForm()
        {
            this.Text = string.Format("{0} [{1}] Help", _transformation.Name, _transformation.Code);

            pictureBoxUseMultipleTimes.Image = (_transformation.UseOnlyOnce) ? Properties.Resources.the_delete_icon : Properties.Resources.check;
            pictureBoxSourceColumnDefined.Image = (_transformation.HasSourceColumn) ? Properties.Resources.check : Properties.Resources.the_delete_icon;

            switch (_transformation.Code)
            {
                case ETransformationCode.BUSINESS_KEY:
                    textBoxDesc.Text = "Business keys are used to connect source and target (or history) table by uniquely identifying single record in both tables. At least one Business Key transformation must be defined, but of course, there can be more, depending on your data.";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.CREATED_DATE:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.DELETED_DATE:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.IS_DELETED:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.MODIFIED_DATE:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD0:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD1:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD2:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD2_DATE_FROM:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD2_DATE_TO:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD2_IS_ACTIVE:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD3_CURRENT_VALUE:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD3_DATE_FROM:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SCD3_PREVIOUS_VALUE:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.SKIP:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
                case ETransformationCode.VERSION_NUMBER:
                    textBoxDesc.Text = "";
                    textBoxOnInsert.Text = "";
                    textBoxOnUpdate.Text = "";
                    textBoxOnDelete.Text = "";
                    break;
            }

            textBoxOnInsert.Text = string.Format("");
            /*

                string title = string.Format("{0} [{1}] Help", trf.Name, trf.Code);
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(string.Format("Transformation {0} [{1}] properties:", trf.Name, trf.Code));
                sb.AppendLine();
                sb.AppendLine(string.Format(" - {0} be used multimple times", trf.UseOnlyOnce ? "Cannot" : "Can"));
                sb.AppendLine();
                sb.AppendLine(string.Format(" - Source column {0} to be defined", trf.HasSourceColumn ? "has" : "does not have"));

                if (!string.IsNullOrEmpty(trf.OnInsertColumnDesc))
                {
                    sb.AppendLine();
                    sb.AppendLine(string.Format(" - On Insert column:{1}{0}", trf.OnInsertColumnDesc, Environment.NewLine));
                }
                if (!string.IsNullOrEmpty(trf.OnUpdateColumnDesc))
                {
                    sb.AppendLine();
                    sb.AppendLine(string.Format(" - On Update column:{1}{0}", trf.OnUpdateColumnDesc, Environment.NewLine));
                }
                if (!string.IsNullOrEmpty(trf.OnDeleteColumnDesc))
                {
                    sb.AppendLine();
                    sb.AppendLine(string.Format(" - On Delete column:{1}{0}", trf.OnDeleteColumnDesc, Environment.NewLine));
                }
                MessageBox.Show(sb.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                 * */
        }
    }
}
