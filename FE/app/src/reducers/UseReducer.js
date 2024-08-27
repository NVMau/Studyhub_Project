
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

export default UseReducer