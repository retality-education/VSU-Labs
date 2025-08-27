using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchool
{
    public class MusicSchoolController
    {
        private MusicSchool _model;
        private MusicSchoolControl _view;

        public MusicSchoolController(MusicSchool model, MusicSchoolControl view)
        {
            _model = model;
            _view = view;

            _model.Subscribe(_view);

            Configure();
        }

        public void Configure()
        {
            _view.form.Controller = this;
            _view.form.Controls.Add(_view);
        }
        public async Task StartLessons()
        {
            await _model.StartMusicLessons();
        }
    }
}
