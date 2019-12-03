TSFRepository.registerComponent(class NavigationContainer extends TSFComponent {
    constructor() {
        super();

        this.state.display = "ViewDaily";
        this.state.menu = true;
    }

    navigate(e, target) {
        this.state.display = target;
    }

    menuClick() {
        this.state.menu = !this.state.menu;
    }
});