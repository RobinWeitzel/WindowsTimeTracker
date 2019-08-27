TSFRepository.registerComponent(class NavigationContainer extends TSFComponent {
    constructor() {
        super();

        this.state.display = "ViewDaily";
    }

    navigate(e, target) {
        this.state.display = target;
    }
});