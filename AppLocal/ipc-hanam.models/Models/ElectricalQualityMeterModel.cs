using AFSClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class ElectricalQualityMeterModel : INotifyPropertyChanged
    {
        string _tagPrefix;
        string _status = "Offline";

        public string DisplayName { get; set; }
        public int Index { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string TagPrefix
        {
            get => _tagPrefix;
            set
            {
                if (_tagPrefix != value)
                {
                    _tagPrefix = value;
                    RaisePropertyChanged();
                    this.LoadTags(true);
                    if (PA != null)
                    {
                        PA.QualityChanged += PA_QualityChanged;
                    }
                    PA_QualityChanged(null, null);
                }
            }
        }
        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged();
                }
            }
        }
        public MeterType MeterType { get; set; } = MeterType.ElectricalQuality;

        public TagNode PA { get; set; }
        public TagNode PA1 { get; set; }
        public TagNode PAH { get; set; }
        public TagNode QA { get; set; }
        public TagNode QA1 { get; set; }
        public TagNode QAH { get; set; }
        public TagNode PB { get; set; }
        public TagNode PB1 { get; set; }
        public TagNode PBH { get; set; }
        public TagNode QB { get; set; }
        public TagNode QB1 { get; set; }
        public TagNode QBH { get; set; }
        public TagNode PC { get; set; }
        public TagNode PC1 { get; set; }
        public TagNode PCH { get; set; }
        public TagNode QC { get; set; }
        public TagNode QC1 { get; set; }
        public TagNode QCH { get; set; }
        public TagNode Cosa { get; set; }
        public TagNode COSB { get; set; }
        public TagNode COSC { get; set; }
        public TagNode Cosxa { get; set; }
        public TagNode Cosxb { get; set; }
        public TagNode Cosxc { get; set; }
        public TagNode FREQ { get; set; }
        public TagNode UA { get; set; }
        public TagNode UB { get; set; }
        public TagNode UC { get; set; }
        public TagNode IA { get; set; }
        public TagNode IB { get; set; }
        public TagNode IC { get; set; }
        public TagNode UA1 { get; set; }
        public TagNode UB1 { get; set; }
        public TagNode UC1 { get; set; }
        public TagNode IA1 { get; set; }
        public TagNode IB1 { get; set; }
        public TagNode IC1 { get; set; }
        public TagNode Hrua2 { get; set; }
        public TagNode Hrua3 { get; set; }
        public TagNode Hrua4 { get; set; }
        public TagNode Hrua5 { get; set; }
        public TagNode Hrua6 { get; set; }
        public TagNode Hrua7 { get; set; }
        public TagNode Hrua8 { get; set; }
        public TagNode Hrua9 { get; set; }
        public TagNode Hrua10 { get; set; }
        public TagNode Hrua11 { get; set; }
        public TagNode Hrua12 { get; set; }
        public TagNode Hrua13 { get; set; }
        public TagNode Hrua14 { get; set; }
        public TagNode Hrua15 { get; set; }
        public TagNode Hrua16 { get; set; }
        public TagNode Hrua17 { get; set; }
        public TagNode Hrua18 { get; set; }
        public TagNode Hrua19 { get; set; }
        public TagNode Hrua20 { get; set; }
        public TagNode Hrua21 { get; set; }
        public TagNode Hrua22 { get; set; }
        public TagNode Hrua23 { get; set; }
        public TagNode Hrua24 { get; set; }
        public TagNode Hrua25 { get; set; }
        public TagNode Hrua26 { get; set; }
        public TagNode Hrua27 { get; set; }
        public TagNode Hrua28 { get; set; }
        public TagNode Hrua29 { get; set; }
        public TagNode Hrua30 { get; set; }
        public TagNode Hrua31 { get; set; }
        public TagNode Hrua32 { get; set; }
        public TagNode Hrua33 { get; set; }
        public TagNode Hrua34 { get; set; }
        public TagNode Hrua35 { get; set; }
        public TagNode Hrua36 { get; set; }
        public TagNode Hrua37 { get; set; }
        public TagNode Hrua38 { get; set; }
        public TagNode Hrua39 { get; set; }
        public TagNode Hrua40 { get; set; }
        public TagNode Hrua41 { get; set; }
        public TagNode Hrua42 { get; set; }
        public TagNode Hrua43 { get; set; }
        public TagNode Hrua44 { get; set; }
        public TagNode Hrua45 { get; set; }
        public TagNode Hrua46 { get; set; }
        public TagNode Hrua47 { get; set; }
        public TagNode Hrua48 { get; set; }
        public TagNode Hrua49 { get; set; }
        public TagNode Hrua50 { get; set; }
        public TagNode Hrua51 { get; set; }
        public TagNode Hrua52 { get; set; }
        public TagNode Hrua53 { get; set; }
        public TagNode Hrua54 { get; set; }
        public TagNode Hrua55 { get; set; }
        public TagNode Hrua56 { get; set; }
        public TagNode Hrua57 { get; set; }
        public TagNode Hrua58 { get; set; }
        public TagNode Hrua59 { get; set; }
        public TagNode Hrua60 { get; set; }
        public TagNode Hrua61 { get; set; }
        public TagNode Hrua62 { get; set; }
        public TagNode Hrua63 { get; set; }
        public TagNode HRUB2 { get; set; }
        public TagNode Hrub3 { get; set; }
        public TagNode Hrub4 { get; set; }
        public TagNode HRUB5 { get; set; }
        public TagNode Hrub6 { get; set; }
        public TagNode HRUB7 { get; set; }
        public TagNode Hrub8 { get; set; }
        public TagNode Hrub9 { get; set; }
        public TagNode Hrub10 { get; set; }
        public TagNode Hrub11 { get; set; }
        public TagNode HRUB12 { get; set; }
        public TagNode HRUB13 { get; set; }
        public TagNode HRUB14 { get; set; }
        public TagNode HRUB15 { get; set; }
        public TagNode HRUB16 { get; set; }
        public TagNode HRUB17 { get; set; }
        public TagNode HRUB18 { get; set; }
        public TagNode Hrub19 { get; set; }
        public TagNode HRUB20 { get; set; }
        public TagNode HRUB21 { get; set; }
        public TagNode HRUB22 { get; set; }
        public TagNode HRUB23 { get; set; }
        public TagNode HRUB24 { get; set; }
        public TagNode HRUB25 { get; set; }
        public TagNode HRUB26 { get; set; }
        public TagNode HRUB27 { get; set; }
        public TagNode HRUB28 { get; set; }
        public TagNode Hrub29 { get; set; }
        public TagNode HRUB30 { get; set; }
        public TagNode HRUB31 { get; set; }
        public TagNode HRUB32 { get; set; }
        public TagNode HRUB33 { get; set; }
        public TagNode HRUB34 { get; set; }
        public TagNode HRUB35 { get; set; }
        public TagNode HRUB36 { get; set; }
        public TagNode HRUB37 { get; set; }
        public TagNode HRUB38 { get; set; }
        public TagNode HRUB39 { get; set; }
        public TagNode HRUB40 { get; set; }
        public TagNode HRUB41 { get; set; }
        public TagNode HRUB42 { get; set; }
        public TagNode HRUB43 { get; set; }
        public TagNode HRUB44 { get; set; }
        public TagNode HRUB45 { get; set; }
        public TagNode HRUB46 { get; set; }
        public TagNode HRUB47 { get; set; }
        public TagNode HRUB48 { get; set; }
        public TagNode HRUB49 { get; set; }
        public TagNode Hrub50 { get; set; }
        public TagNode HRUB51 { get; set; }
        public TagNode HRUB52 { get; set; }
        public TagNode HRUB53 { get; set; }
        public TagNode HRUB54 { get; set; }
        public TagNode HRUB55 { get; set; }
        public TagNode HRUB56 { get; set; }
        public TagNode HRUB57 { get; set; }
        public TagNode HRUB58 { get; set; }
        public TagNode HRUB59 { get; set; }
        public TagNode HRUB60 { get; set; }
        public TagNode HRUB61 { get; set; }
        public TagNode HRUB62 { get; set; }
        public TagNode HRUB63 { get; set; }
        public TagNode Hruc2 { get; set; }
        public TagNode Hruc3 { get; set; }
        public TagNode Hruc4 { get; set; }
        public TagNode Hruc5 { get; set; }
        public TagNode Hruc6 { get; set; }
        public TagNode Hruc7 { get; set; }
        public TagNode Hruc8 { get; set; }
        public TagNode Hruc9 { get; set; }
        public TagNode Hruc10 { get; set; }
        public TagNode Hruc11 { get; set; }
        public TagNode Hruc12 { get; set; }
        public TagNode Hruc13 { get; set; }
        public TagNode Hruc14 { get; set; }
        public TagNode Hruc15 { get; set; }
        public TagNode Hruc16 { get; set; }
        public TagNode Hruc17 { get; set; }
        public TagNode Hruc18 { get; set; }
        public TagNode Hruc19 { get; set; }
        public TagNode Hruc20 { get; set; }
        public TagNode Hruc21 { get; set; }
        public TagNode HRUC22 { get; set; }
        public TagNode Hruc23 { get; set; }
        public TagNode Hruc24 { get; set; }
        public TagNode Hruc25 { get; set; }
        public TagNode Hruc26 { get; set; }
        public TagNode Hruc27 { get; set; }
        public TagNode Hruc28 { get; set; }
        public TagNode Hruc29 { get; set; }
        public TagNode Hruc30 { get; set; }
        public TagNode Hruc31 { get; set; }
        public TagNode Hruc32 { get; set; }
        public TagNode Hruc33 { get; set; }
        public TagNode Hruc34 { get; set; }
        public TagNode Hruc35 { get; set; }
        public TagNode Hruc36 { get; set; }
        public TagNode Hruc37 { get; set; }
        public TagNode Hruc38 { get; set; }
        public TagNode Hruc39 { get; set; }
        public TagNode Hruc40 { get; set; }
        public TagNode Hruc41 { get; set; }
        public TagNode Hruc42 { get; set; }
        public TagNode Hruc43 { get; set; }
        public TagNode Hruc44 { get; set; }
        public TagNode Hruc45 { get; set; }
        public TagNode Hruc46 { get; set; }
        public TagNode Hruc47 { get; set; }
        public TagNode Hruc48 { get; set; }
        public TagNode Hruc49 { get; set; }
        public TagNode Hruc50 { get; set; }
        public TagNode Hruc51 { get; set; }
        public TagNode Hruc52 { get; set; }
        public TagNode Hruc53 { get; set; }
        public TagNode Hruc54 { get; set; }
        public TagNode Hruc55 { get; set; }
        public TagNode Hruc56 { get; set; }
        public TagNode Hruc57 { get; set; }
        public TagNode Hruc58 { get; set; }
        public TagNode Hruc59 { get; set; }
        public TagNode Hruc60 { get; set; }
        public TagNode Hruc61 { get; set; }
        public TagNode Hruc62 { get; set; }
        public TagNode Hruc63 { get; set; }
        public TagNode HRIA2 { get; set; }
        public TagNode HRIA3 { get; set; }
        public TagNode Hria4 { get; set; }
        public TagNode HRIA5 { get; set; }
        public TagNode HRIA6 { get; set; }
        public TagNode HRIA7 { get; set; }
        public TagNode HRIA8 { get; set; }
        public TagNode Hria9 { get; set; }
        public TagNode HRIA10 { get; set; }
        public TagNode HRIA11 { get; set; }
        public TagNode HRIA12 { get; set; }
        public TagNode Hria13 { get; set; }
        public TagNode HRIA14 { get; set; }
        public TagNode HRIA15 { get; set; }
        public TagNode HRIA16 { get; set; }
        public TagNode HRIA17 { get; set; }
        public TagNode HRIA18 { get; set; }
        public TagNode HRIA19 { get; set; }
        public TagNode HRIA20 { get; set; }
        public TagNode HRIA21 { get; set; }
        public TagNode HRIA22 { get; set; }
        public TagNode HRIA23 { get; set; }
        public TagNode Hria24 { get; set; }
        public TagNode HRIA25 { get; set; }
        public TagNode HRIA26 { get; set; }
        public TagNode HRIA27 { get; set; }
        public TagNode HRIA28 { get; set; }
        public TagNode HRIA29 { get; set; }
        public TagNode Hria30 { get; set; }
        public TagNode HRIA31 { get; set; }
        public TagNode HRIA32 { get; set; }
        public TagNode HRIA33 { get; set; }
        public TagNode HRIA34 { get; set; }
        public TagNode HRIA35 { get; set; }
        public TagNode HRIA36 { get; set; }
        public TagNode HRIA37 { get; set; }
        public TagNode HRIA38 { get; set; }
        public TagNode HRIA39 { get; set; }
        public TagNode HRIA40 { get; set; }
        public TagNode HRIA41 { get; set; }
        public TagNode HRIA42 { get; set; }
        public TagNode HRIA43 { get; set; }
        public TagNode HRIA44 { get; set; }
        public TagNode HRIA45 { get; set; }
        public TagNode HRIA46 { get; set; }
        public TagNode Hria47 { get; set; }
        public TagNode HRIA48 { get; set; }
        public TagNode HRIA49 { get; set; }
        public TagNode HRIA50 { get; set; }
        public TagNode HRIA51 { get; set; }
        public TagNode HRIA52 { get; set; }
        public TagNode HRIA53 { get; set; }
        public TagNode HRIA54 { get; set; }
        public TagNode HRIA55 { get; set; }
        public TagNode HRIA56 { get; set; }
        public TagNode HRIA57 { get; set; }
        public TagNode HRIA58 { get; set; }
        public TagNode HRIA59 { get; set; }
        public TagNode HRIA60 { get; set; }
        public TagNode HRIA61 { get; set; }
        public TagNode HRIA62 { get; set; }
        public TagNode HRIA63 { get; set; }
        public TagNode HRIB2 { get; set; }
        public TagNode HRIB3 { get; set; }
        public TagNode HRIB4 { get; set; }
        public TagNode HRIB5 { get; set; }
        public TagNode HRIB6 { get; set; }
        public TagNode HRIB7 { get; set; }
        public TagNode HRIB8 { get; set; }
        public TagNode HRIB9 { get; set; }
        public TagNode HRIB10 { get; set; }
        public TagNode HRIB11 { get; set; }
        public TagNode HRIB12 { get; set; }
        public TagNode HRIB13 { get; set; }
        public TagNode HRIB14 { get; set; }
        public TagNode HRIB15 { get; set; }
        public TagNode HRIB16 { get; set; }
        public TagNode HRIB17 { get; set; }
        public TagNode HRIB18 { get; set; }
        public TagNode HRIB19 { get; set; }
        public TagNode HRIB20 { get; set; }
        public TagNode HRIB21 { get; set; }
        public TagNode HRIB22 { get; set; }
        public TagNode HRIB23 { get; set; }
        public TagNode HRIB24 { get; set; }
        public TagNode HRIB25 { get; set; }
        public TagNode HRIB26 { get; set; }
        public TagNode HRIB27 { get; set; }
        public TagNode HRIB28 { get; set; }
        public TagNode HRIB29 { get; set; }
        public TagNode HRIB30 { get; set; }
        public TagNode HRIB31 { get; set; }
        public TagNode HRIB32 { get; set; }
        public TagNode HRIB33 { get; set; }
        public TagNode HRIB34 { get; set; }
        public TagNode HRIB35 { get; set; }
        public TagNode HRIB36 { get; set; }
        public TagNode HRIB37 { get; set; }
        public TagNode HRIB38 { get; set; }
        public TagNode HRIB39 { get; set; }
        public TagNode HRIB40 { get; set; }
        public TagNode HRIB41 { get; set; }
        public TagNode HRIB42 { get; set; }
        public TagNode HRIB43 { get; set; }
        public TagNode HRIB44 { get; set; }
        public TagNode HRIB45 { get; set; }
        public TagNode HRIB46 { get; set; }
        public TagNode HRIB47 { get; set; }
        public TagNode HRIB48 { get; set; }
        public TagNode HRIB49 { get; set; }
        public TagNode HRIB50 { get; set; }
        public TagNode HRIB51 { get; set; }
        public TagNode HRIB52 { get; set; }
        public TagNode HRIB53 { get; set; }
        public TagNode HRIB54 { get; set; }
        public TagNode HRIB55 { get; set; }
        public TagNode HRIB56 { get; set; }
        public TagNode HRIB57 { get; set; }
        public TagNode HRIB58 { get; set; }
        public TagNode HRIB59 { get; set; }
        public TagNode HRIB60 { get; set; }
        public TagNode HRIB61 { get; set; }
        public TagNode HRIB62 { get; set; }
        public TagNode HRIB63 { get; set; }
        public TagNode Hric2 { get; set; }
        public TagNode Hric3 { get; set; }
        public TagNode Hric4 { get; set; }
        public TagNode Hric5 { get; set; }
        public TagNode Hric6 { get; set; }
        public TagNode Hric7 { get; set; }
        public TagNode Hric8 { get; set; }
        public TagNode Hric9 { get; set; }
        public TagNode Hric10 { get; set; }
        public TagNode Hric11 { get; set; }
        public TagNode Hric12 { get; set; }
        public TagNode Hric13 { get; set; }
        public TagNode Hric14 { get; set; }
        public TagNode Hric15 { get; set; }
        public TagNode Hric16 { get; set; }
        public TagNode Hric17 { get; set; }
        public TagNode Hric18 { get; set; }
        public TagNode Hric19 { get; set; }
        public TagNode Hric20 { get; set; }
        public TagNode Hric21 { get; set; }
        public TagNode Hric22 { get; set; }
        public TagNode Hric23 { get; set; }
        public TagNode Hric24 { get; set; }
        public TagNode Hric25 { get; set; }
        public TagNode Hric26 { get; set; }
        public TagNode Hric27 { get; set; }
        public TagNode Hric28 { get; set; }
        public TagNode Hric29 { get; set; }
        public TagNode Hric30 { get; set; }
        public TagNode Hric31 { get; set; }
        public TagNode Hric32 { get; set; }
        public TagNode Hric33 { get; set; }
        public TagNode Hric34 { get; set; }
        public TagNode Hric35 { get; set; }
        public TagNode Hric36 { get; set; }
        public TagNode Hric37 { get; set; }
        public TagNode Hric38 { get; set; }
        public TagNode Hric39 { get; set; }
        public TagNode Hric40 { get; set; }
        public TagNode Hric41 { get; set; }
        public TagNode Hric42 { get; set; }
        public TagNode Hric43 { get; set; }
        public TagNode Hric44 { get; set; }
        public TagNode Hric45 { get; set; }
        public TagNode Hric46 { get; set; }
        public TagNode Hric47 { get; set; }
        public TagNode Hric48 { get; set; }
        public TagNode Hric49 { get; set; }
        public TagNode Hric50 { get; set; }
        public TagNode Hric51 { get; set; }
        public TagNode Hric52 { get; set; }
        public TagNode Hric53 { get; set; }
        public TagNode Hric54 { get; set; }
        public TagNode Hric55 { get; set; }
        public TagNode Hric56 { get; set; }
        public TagNode Hric57 { get; set; }
        public TagNode Hric58 { get; set; }
        public TagNode Hric59 { get; set; }
        public TagNode Hric60 { get; set; }
        public TagNode Hric61 { get; set; }
        public TagNode Hric62 { get; set; }
        public TagNode Hric63 { get; set; }
        public TagNode THDUA { get; set; }
        public TagNode THDUB { get; set; }
        public TagNode THDUC { get; set; }
        public TagNode THDIA { get; set; }
        public TagNode THDIB { get; set; }
        public TagNode THDIC { get; set; }
        public TagNode U1 { get; set; }
        public TagNode U2 { get; set; }
        public TagNode U0 { get; set; }
        public TagNode I1 { get; set; }
        public TagNode I2 { get; set; }
        public TagNode I0 { get; set; }
        public TagNode VAD { get; set; }
        public TagNode VBD { get; set; }
        public TagNode VCD { get; set; }
        public TagNode RDVA { get; set; }
        public TagNode RDVB { get; set; }
        public TagNode RDVC { get; set; }
        public TagNode PSTA { get; set; }
        public TagNode PSTB { get; set; }
        public TagNode PSTC { get; set; }
        public TagNode PLTA { get; set; }
        public TagNode PLTB { get; set; }
        public TagNode PLTC { get; set; }
        public TagNode YX_State { get; set; }
        public TagNode UA1X { get; set; }
        public TagNode UA1Y { get; set; }
        public TagNode UB1X { get; set; }
        public TagNode Ub1y { get; set; }
        public TagNode UC1X { get; set; }
        public TagNode UC1Y { get; set; }
        public TagNode Hrua0 { get; set; }
        public TagNode Hrub0 { get; set; }
        public TagNode Hruc0 { get; set; }
        public TagNode HRIA0 { get; set; }
        public TagNode HRIB0 { get; set; }
        public TagNode Hric0 { get; set; }
        public TagNode U2u1 { get; set; }
        public TagNode I2i1 { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PA_QualityChanged(object sender, TagQualityChangedEventArgs e)
        {
            if (PA != null)
            {
                if (PA.Quality == Quality.Good)
                {
                    Status = "Online";
                }
                else
                {
                    Status = "Offline";
                }
            }
            else
            {
                Status = "Offline";
            }
        }
    }
}
