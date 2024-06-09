using UnityEngine;
using Zenject;

namespace Core.Services {
	public class ProjectContextInstaller : MonoInstaller {
		[SerializeField] private AudioPlayer audioPlayer;
		
		public override void InstallBindings() {
			Container.Bind<ProgressSaver>().AsSingle();
			Container.Bind<AudioPlayer>().FromInstance(audioPlayer).AsSingle();
		}
	}
}