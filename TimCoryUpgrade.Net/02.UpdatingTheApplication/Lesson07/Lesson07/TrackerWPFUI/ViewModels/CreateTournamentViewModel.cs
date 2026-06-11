using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerWPFUI.ViewModels
{
    public class CreateTournamentViewModel : Conductor<object>.Collection.AllActive, IHandle<TeamModel>, IHandle<PrizeModel>
    {
        private string _tournamentName = "";
        private decimal _entryFee;
        private BindableCollection<TeamModel> _availableTeams;
        private TeamModel _selectedTeamToAdd;
        private BindableCollection<TeamModel> _selectedTeams = new BindableCollection<TeamModel>();
        private TeamModel _selectedTeamToRemove;
        private Screen _activeAddTeamView;
        private BindableCollection<PrizeModel> _selectedPrizes = new BindableCollection<PrizeModel>();
        private PrizeModel _selectedPrizeToRemove;
        private Screen _activeAddPrizeView;
        private bool _selectedTeamsIsVisible = true;
        private bool _addTeamIsVisible = false;
        private bool _selectedPrizesIsVisible = true;
        private bool _addPrizeIsVisible = false;

        public CreateTournamentViewModel()
        {
            AvailableTeams = new BindableCollection<TeamModel>(GlobalConfig.Connection.GetTeam_All());
            EventAggregationProvider.TrackerEventAggregor.Subscribe(this);
        }

        public string TournamentName
        {
            get { return _tournamentName; }
            set
            {
                _tournamentName = value;
                NotifyOfPropertyChange(() => TournamentName);
                NotifyOfPropertyChange(() => CanCreateTournament);
            }
        }

        public decimal EntryFee
        {
            get { return _entryFee; }
            set
            {
                _entryFee = value;
                NotifyOfPropertyChange(() => EntryFee);
            }
        }
        
        public BindableCollection<TeamModel> AvailableTeams
        {
            get { return _availableTeams; }
            set { _availableTeams = value; }
        }

        public TeamModel SelectedTeamToAdd
        {
            get { return _selectedTeamToAdd; }
            set
            {
                _selectedTeamToAdd = value;
                NotifyOfPropertyChange(() => SelectedTeamToAdd);
                NotifyOfPropertyChange(() => CanAddTeam);
            }
        }

        public BindableCollection<TeamModel> SelectedTeams
        {
            get { return _selectedTeams; }
            set
            {
                _selectedTeams = value;
                NotifyOfPropertyChange(() => SelectedTeams);
            }
        }

        public TeamModel SelectedTeamToRemove
        {
            get { return _selectedTeamToRemove; }
            set
            {
                _selectedTeamToRemove = value;
                NotifyOfPropertyChange(() => SelectedTeamToRemove);
                NotifyOfPropertyChange(() => CanRemoveTeam);
            }
        }

        public Screen ActiveAddTeamView
        {
            get { return _activeAddTeamView; }
            set
            {
                _activeAddTeamView = value;
                NotifyOfPropertyChange(() => ActiveAddTeamView);
            }
        }

        public BindableCollection<PrizeModel> SelectedPrizes
        {
            get { return _selectedPrizes; }
            set { _selectedPrizes = value; }
        }

        public PrizeModel SelectedPrizeToRemove
        {
            get { return _selectedPrizeToRemove; }
            set
            {
                _selectedPrizeToRemove = value;
                NotifyOfPropertyChange(() => SelectedPrizeToRemove);
                NotifyOfPropertyChange(() => CanRemovePrize);
            }
        }

        public Screen ActiveAddPrizeView
        {
            get { return _activeAddPrizeView; }
            set
            {
                _activeAddPrizeView = value;
                NotifyOfPropertyChange(() => ActiveAddPrizeView);
            }
        }

        public bool SelectedTeamsIsVisible
        {
            get { return _selectedTeamsIsVisible; }
            set
            {
                _selectedTeamsIsVisible = value;
                NotifyOfPropertyChange(() => SelectedTeamsIsVisible);
            }
        }

        public bool AddTeamIsVisible
        {
            get { return _addTeamIsVisible; }
            set
            {
                _addTeamIsVisible = value;
                NotifyOfPropertyChange(() => AddTeamIsVisible);
            }
        }

        public bool SelectedPrizesIsVisible
        {
            get { return _selectedPrizesIsVisible; }
            set
            {
                _selectedPrizesIsVisible = value;
                NotifyOfPropertyChange(() => SelectedPrizesIsVisible);
            }
        }

        public bool AddPrizeIsVisible
        {
            get { return _addPrizeIsVisible; }
            set
            {
                _addPrizeIsVisible = value;
                NotifyOfPropertyChange(() => AddPrizeIsVisible);
            }
        }
        
        public bool CanAddTeam
        {
            get
            {
                return SelectedTeamToAdd != null;
            }
        }

        public void AddTeam()
        {
            SelectedTeams.Add(SelectedTeamToAdd);
            AvailableTeams.Remove(SelectedTeamToAdd);
            NotifyOfPropertyChange(() => CanCreateTournament);
        }

        public void CreateTeam()
        {
            ActiveAddTeamView = new CreateTeamViewModel();
            Items.Add(ActiveAddTeamView);

            SelectedTeamsIsVisible = false;
            AddTeamIsVisible = true;
        }

        public bool CanRemoveTeam
        {
            get
            {
                return SelectedTeamToRemove != null;
            }
        }

        public void RemoveTeam()
        {
            AvailableTeams.Add(SelectedTeamToRemove);
            SelectedTeams.Remove(SelectedTeamToRemove);
            NotifyOfPropertyChange(() => CanCreateTournament);
        }

        public void CreatePrize()
        {
            ActiveAddPrizeView = new CreatePrizeViewModel();
            Items.Add(ActiveAddPrizeView);

            SelectedPrizesIsVisible = false;
            AddPrizeIsVisible = true;
        }

        public bool CanRemovePrize
        {
            get
            {
                return SelectedPrizeToRemove != null;
            }
        }

        public void RemovePrize()
        {
            SelectedPrizes.Remove(SelectedPrizeToRemove);
        }

        public bool CanCreateTournament
        {
            get
            {
                if (SelectedTeams != null)
                {
                    if (TournamentName.Length > 0 && SelectedTeams.Count > 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public void CreateTournament()
        {
            // Create our tournament model
            TournamentModel tm = new TournamentModel();

            tm.TournamentName = TournamentName;
            tm.EntryFee = EntryFee;

            tm.Prizes = SelectedPrizes.ToList();
            tm.EnteredTeams = SelectedTeams.ToList();

            // Wire our matchups
            TournamentLogic.CreateRounds(tm);

            // Create Tournament entry
            // Create all of the prizes entries
            // Create all of team entries
            GlobalConfig.Connection.CreateTournament(tm);

            tm.AlertUsersToNewRound();

            EventAggregationProvider.TrackerEventAggregor.PublishOnUIThread(tm);
            this.TryClose();
        }

        public void Handle(TeamModel message)
        {
            if (!String.IsNullOrWhiteSpace(message.TeamName))
            {
                SelectedTeams.Add(message);
                NotifyOfPropertyChange(() => CanCreateTournament);
            }

            SelectedTeamsIsVisible = true;
            AddTeamIsVisible = false;
        }

        public void Handle(PrizeModel message)
        {
            if (!String.IsNullOrWhiteSpace(message.PlaceName))
            {
                SelectedPrizes.Add(message);
            }

            SelectedPrizesIsVisible = true;
            AddPrizeIsVisible = false;
        }
    }
}
