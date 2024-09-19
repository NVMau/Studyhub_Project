const UserReducer = (user, action) => {
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
    }

    return user;
}

export default UserReducer





// switch (action.type) {
//     case "login": {
//         localStorage.setItem('user', JSON.stringify(userTemp));
//         return JSON.parse(localStorage.getItem('user'))
//     }
//     // return userTemp
//     case "logout": {
//         localStorage.removeItem('user');
//         return null;
//     }
//     case "upstore":
//         return JSON.parse(localStorage.getItem('user'))
// }