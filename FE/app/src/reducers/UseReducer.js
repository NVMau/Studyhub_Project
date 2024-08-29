<<<<<<< HEAD

const UseReducer = (user, action) => {
    switch (action.type) {
        case "login": {
            localStorage.setItem('user', JSON.stringify(action.payload));
            return action.payload
        }
        case "logout": {
            localStorage.removeItem('user');
            return null;
        }
        case "upstore":
            return JSON.parse(localStorage.getItem('user'))

        default: 
            return user;
    }
}

=======

const UseReducer = (user, action) => {
    switch (action.type) {
        case "login": {
            localStorage.setItem('user', JSON.stringify(action.payload));
            return action.payload
        }
        case "logout": {
            localStorage.removeItem('user');
            return null;
        }
        case "upstore":
            return JSON.parse(localStorage.getItem('user'))

        default: 
            return user;
    }
}

>>>>>>> 4b7ae76bcaddb780f7cbf1f931b9e71a240a5481
export default UseReducer